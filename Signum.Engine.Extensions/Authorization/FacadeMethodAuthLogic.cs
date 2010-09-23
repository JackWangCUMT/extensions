﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Engine.Maps;
using Signum.Entities.Authorization;
using Signum.Entities.Basics;
using Signum.Engine.DynamicQuery;
using Signum.Engine.Basics;
using System.Reflection;
using Signum.Utilities;
using Signum.Entities;
using Signum.Services;
using Signum.Engine.Extensions.Properties;

namespace Signum.Engine.Authorization
{
    public static class FacadeMethodAuthLogic
    {
        static AuthCache<RuleFacadeMethodDN, FacadeMethodAllowedRule, FacadeMethodDN, string, bool> cache;

        public static IManualAuth<string, bool> Manual { get { return cache; } }

        public static bool IsStarted { get { return cache != null; } }

        public static void Start(SchemaBuilder sb, Type serviceInterface)
        {
            if (sb.NotDefined(MethodInfo.GetCurrentMethod()))
            {
                AuthLogic.AssertStarted(sb);
                FacadeMethodLogic.Start(sb, serviceInterface);

                cache = new AuthCache<RuleFacadeMethodDN, FacadeMethodAllowedRule, FacadeMethodDN, string, bool>(sb,
                     fm => fm.Name,
                     n => FacadeMethodLogic.RetrieveOrGenerateServiceOperation(n),
                     AuthUtils.MaxBool,
                     AuthUtils.MinBool); 
            }
        }

        public static FacadeMethodRulePack GetFacadeMethodRules(Lite<RoleDN> roleLite)
        {
            FacadeMethodRulePack result = new FacadeMethodRulePack { Role = roleLite }; 
            cache.GetRules(result, FacadeMethodLogic.RetrieveOrGenerateServiceOperations()) ;
            return result; 
        }

        public static void SetFacadeMethodRules(FacadeMethodRulePack rules)
        {
            cache.SetRules(rules, r => true);
        }

      
        public static bool SetFacadeMethodAllowed(Lite<RoleDN> role, MethodInfo mi)
        {
            return cache.GetAllowed(role, mi.Name);
        }

        public static bool SetFacadeMethodAllowed(MethodInfo mi)
        {
            return cache.GetAllowed(mi.Name);
        }

        public static void AuthorizeAccess(MethodInfo mi)
        {
            if (!cache.GetAllowed(mi.Name))
                throw new UnauthorizedAccessException(Resources.AccessToFacadeMethod0IsNotAllowed.Formato(mi.Name));
        }
    }
}
