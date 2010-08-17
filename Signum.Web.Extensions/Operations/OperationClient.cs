﻿#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Signum.Engine.Operations;
using Signum.Entities.Operations;
using Signum.Utilities;
using Signum.Entities;
using System.Web;
using Signum.Entities.Basics;
using Signum.Web.Extensions.Properties;
#endregion

namespace Signum.Web.Operations
{
    public static class OperationClient
    {
        public static OperationManager Manager { get; private set; }

        public static void Start(OperationManager operationManager)
        {
            Manager = operationManager;

            ButtonBarEntityHelper.RegisterGlobalButtons(Manager.ButtonBar_GetButtonBarElement);

            Constructor.ConstructorManager.GeneralConstructor += new Func<Type, ModifiableEntity>(Manager.ConstructorManager_GeneralConstructor);
            Constructor.ConstructorManager.VisualGeneralConstructor += new Func<ConstructContext, ActionResult>(Manager.ConstructorManager_VisualGeneralConstructor); 
            ButtonBarQueryHelper.GetButtonBarForQueryName += Manager.ButtonBar_GetButtonBarForQueryName;
        }
    }

    public class OperationManager
    {
        public Dictionary<Enum, OperationSettings> Settings = new Dictionary<Enum, OperationSettings>();

        internal ToolBarButton[] ButtonBar_GetButtonBarElement(ControllerContext controllerContext, ModifiableEntity entity, string partialViewName, string prefix)
        {
            IdentifiableEntity ident = entity as IdentifiableEntity;

            if (ident == null)
                return null;

            var list = OperationLogic.ServiceGetEntityOperationInfos(ident);

            var contexts =
                    from oi in list
                    let os = (EntityOperationSettings)Settings.TryGetC(oi.Key)
                    let ctx = new EntityOperationContext
                    {
                         Entity = ident,
                         OperationSettings = os,
                         OperationInfo = oi,
                         PartialViewName = partialViewName,
                         Prefix = prefix
                    }
                    where os == null || os.IsVisible == null || os.IsVisible(ctx)
                    select ctx;

            List<ToolBarButton> buttons = contexts
                .Where(oi => oi.OperationInfo.OperationType != OperationType.ConstructorFrom || 
                            (oi.OperationInfo.OperationType == OperationType.ConstructorFrom && oi.OperationSettings != null && !oi.OperationSettings.GroupInMenu))
                .Select(ctx => OperationButtonFactory.Create(ctx))
                .ToList();

            var constructFroms = contexts.Where(oi => oi.OperationInfo.OperationType == OperationType.ConstructorFrom && 
                            (oi.OperationSettings == null || (oi.OperationSettings != null && oi.OperationSettings.GroupInMenu)));
            if (constructFroms.Any())
            {
                buttons.Add(new ToolBarMenu
                {
                    AltText = Resources.Create,
                    Text = Resources.Create,
                    DivCssClass = ToolBarButton.DefaultEntityDivCssClass,
                    Items = constructFroms.Select(ctx => OperationButtonFactory.Create(ctx)).ToList()
                });
            }

            //List<ToolBarButton> buttons = 
            //        (from oi in list.Where(oi => oi.OperationType != OperationType.ConstructorFrom)
            //        let os = (EntityOperationSettings)Settings.TryGetC(oi.Key)
            //        let ctx = new EntityOperationContext
            //        {
            //            Entity = ident,
            //            OperationSettings = os,
            //            OperationInfo = oi,
            //            PartialViewName = partialViewName,
            //            Prefix = prefix
            //        }
            //        where os == null || os.IsVisible == null || os.IsVisible(ctx)
            //        select OperationButtonFactory.Create(ctx)).ToList();

            //var constructFroms = list.Where(oi => oi.OperationType == OperationType.ConstructorFrom);
            //if (constructFroms.Any())
            //{
            //    buttons.Add(new ToolBarMenu
            //    {
            //        AltText = Resources.Create,
            //        Text = Resources.Create,
            //        DivCssClass = ToolBarButton.DefaultEntityDivCssClass,
            //        Items = 
            //            (from oi in constructFroms
            //            let os = (EntityOperationSettings)Settings.TryGetC(oi.Key)
            //            let ctx = new EntityOperationContext
            //            {
            //                Entity = ident,
            //                OperationSettings = os,
            //                OperationInfo = oi,
            //                PartialViewName = partialViewName,
            //                Prefix = prefix
            //            }
            //            where os == null || os.IsVisible == null || os.IsVisible(ctx)
            //            select OperationButtonFactory.Create(ctx)).ToList()
            //    });
            //}

            return buttons.ToArray();
        }

        internal ToolBarButton[] ButtonBar_GetButtonBarForQueryName(ControllerContext controllerContext, object queryName, Type entityType, string prefix)
        {
            if (entityType == null || queryName == null)
                return null;

            var list = OperationLogic.ServiceGetQueryOperationInfos(entityType);
            return (from oi in list
                    let os = (QueryOperationSettings)Settings.TryGetC(oi.Key)
                    let ctx = new QueryOperationContext
                    {
                        OperationSettings = os,
                        OperationInfo = oi,
                        Prefix = prefix
                    }
                    where os == null || os.IsVisible == null || os.IsVisible(ctx)
                    select OperationButtonFactory.Create(ctx)).ToArray();
        }

        internal ModifiableEntity ConstructorManager_GeneralConstructor(Type type)
        {
            if (!typeof(IIdentifiable).IsAssignableFrom(type))
                return null;

            OperationInfo constructor = OperationLogic.ServiceGetConstructorOperationInfos(type).SingleOrDefault();

            if (constructor == null)
                return null;

            return (ModifiableEntity)OperationLogic.ServiceConstruct(type, constructor.Key);
        }

        internal ActionResult ConstructorManager_VisualGeneralConstructor(ConstructContext ctx)
        {
            var count = OperationLogic.ServiceGetConstructorOperationInfos(ctx.Type).Count;

            if (count == 0 || count == 1)
                return null;

            throw new NotImplementedException();  //show chooser
        }
    }
}
