﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Entities.Omnibox;
using System.Windows.Documents;
using System.Windows.Media;
using Signum.Utilities;

namespace Signum.Windows.Omnibox
{
    public class EntityOmniboxProvider : OmniboxProvider<EntityOmniboxResult>
    {
        public override OmniboxResultGenerator<EntityOmniboxResult> CreateGenerator()
        {
            return new EntityOmniboxResultGenenerator(Server.ServerTypes.Keys);
        }

        public override void RenderLines(EntityOmniboxResult result, InlineCollection lines)
        {
            lines.AddMatch(result.TypeMatch);

            lines.Add(" ");

            if (result.Id == null && result.ToStr == null)
            {
                lines.Add("...");
            }
            else
            {
                if (result.Id != null)
                {
                    lines.Add(result.Id.ToString());
                    lines.Add(": ");
                    if (result.Lite == null)
                    {
                        lines.Add(new Run(Signum.Entities.Extensions.Properties.Resources.NotFound) { Foreground = Brushes.Gray });
                    }
                    else
                    {
                        lines.Add(result.Lite.TryToString());
                    }
                }
                else
                {
                    if (result.Lite == null)
                    {
                        lines.Add("\"");
                        lines.Add(result.ToStr);
                        lines.Add("\": ");
                        lines.Add(new Run(Signum.Entities.Extensions.Properties.Resources.NotFound) { Foreground = Brushes.Gray });
                    }
                    else
                    {
                        lines.Add(result.Lite.Id.ToString());
                        lines.Add(": ");
                        lines.AddMatch(result.ToStrMatch);
                    }
                }
            }

            lines.Add(new Run(" ({0})".Formato(Extensions.Properties.Resources.View)) { Foreground = Brushes.DodgerBlue });
        }

        public override void OnSelected(EntityOmniboxResult result)
        {
            if (result.Lite != null)
                Navigator.NavigateUntyped(result.Lite);
        }
    }
}