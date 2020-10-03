﻿#region Imports

using System.ComponentModel;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Poison
{
    #region PoisonPanelDesignerDesign

    internal class PoisonPanelDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (Control is PoisonPanel)
            {
                //this.EnableDesignMode(((PoisonPanel)this.Control).ScrollablePanel, "ScrollablePanel");
            }
        }
    }

    #endregion
}