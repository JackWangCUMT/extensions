﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using Signum.Entities.Authorization;
using Signum.Entities;
using Signum.Services;
using Signum.Utilities;

namespace Signum.Windows.Authorization
{
    public static class PermissionAuthClient
    {
        public static bool Started { get; private set; }

        static Dictionary<Enum, bool> permissionRules;

        internal static void Start()
        {
            Started = true;

            AuthClient.UpdateCacheEvent += new Action(AuthClient_UpdateCacheEvent);

            Links.RegisterEntityLinks<RoleDN>((r, c) =>
            {
                bool authorized = BasicPermissions.AdminRules.TryIsAuthorized() ?? true;
                return new QuickLink[]
                {
                    new QuickLinkAction("Permission Rules", () => new PermissionRules { Role = r.ToLite(), Owner = c.FindCurrentWindow() }.Show())
                    {
                        IsVisible = authorized
                    },
                };
            }); 
        }

        static void AuthClient_UpdateCacheEvent()
        {
            permissionRules = Server.Return((IPermissionAuthServer s) => s.PermissionRules());
        }

        public static bool? TryIsAuthorized(this Enum permissionKey)
        {
            return permissionRules.TryGetS(permissionKey);
        }

        public static bool IsAuthorized(this Enum permissionKey)
        {
            if (permissionRules == null)
                throw new InvalidOperationException("Permissions not enabled in AuthClient");

            bool result;
            if (!permissionRules.TryGetValue(permissionKey, out result))
                throw new ArgumentException("{0} is not a permissionKey registered in the server".Formato(permissionKey));

            return result;
        }
    }

    [MarkupExtensionReturnType(typeof(bool))]
    public class PermissionExtension : MarkupExtension
    {
        Enum permission;
        public PermissionExtension(object value)
        {
            this.permission = (Enum)value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return permission.IsAuthorized();
        }
    }

    [MarkupExtensionReturnType(typeof(Visibility))]
    public class PermissionVisiblityExtension : MarkupExtension
    {
        Enum permission;
        public PermissionVisiblityExtension(object value)
        {
            this.permission = (Enum)value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return permission.IsAuthorized() ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
