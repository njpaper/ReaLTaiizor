﻿#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using System.Collections.Generic;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Interface.Poison;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonStyleExtender

    [ProvideProperty("ApplyPoisonTheme", typeof(Control))]
    public sealed class PoisonStyleExtender : Component, IExtenderProvider, IPoisonComponent
    {
        #region Interface

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ColorStyle Style
        {
            get { throw new NotSupportedException(); }
            set { }
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                    return poisonTheme;

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                    return StyleManager.Theme;
                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                    return PoisonDefaults.Theme;

                return poisonTheme;
            }
            set
            {
                poisonTheme = value;
                UpdateTheme();
            }
        }

        private PoisonStyleManager poisonStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager
        {
            get { return poisonStyleManager; }
            set
            {
                poisonStyleManager = value;
                UpdateTheme();
            }
        }

        #endregion

        #region Fields

        private readonly List<Control> extendedControls = new List<Control>();

        #endregion

        #region Constructor

        public PoisonStyleExtender()
        {

        }

        public PoisonStyleExtender(IContainer parent) : this()
        {
            if (parent != null)
                parent.Add(this);
        }

        #endregion

        #region Management Methods

        private void UpdateTheme()
        {
            Color backColor = PoisonPaint.BackColor.Form(Theme);
            Color foreColor = PoisonPaint.ForeColor.Label.Normal(Theme);

            foreach (Control ctrl in extendedControls)
            {
                if (ctrl != null)
                {
                    try
                    {
                        ctrl.BackColor = backColor;
                    }
                    catch { }

                    try
                    {
                        ctrl.ForeColor = foreColor;
                    }
                    catch { }
                }
            }
        }

        #endregion

        #region IExtenderProvider

        bool IExtenderProvider.CanExtend(object target)
        {
            return target is Control && !(target is IPoisonControl || target is IPoisonForm);
        }

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [Description("Apply Poison Theme BackColor and ForeColor.")]
        public bool GetApplyPoisonTheme(Control control)
        {
            return control != null && extendedControls.Contains(control);
        }

        public void SetApplyPoisonTheme(Control control, bool value)
        {
            if (control == null)
                return;

            if (extendedControls.Contains(control))
            {
                if (!value)
                    extendedControls.Remove(control);
            }
            else
            {
                if (value)
                    extendedControls.Add(control);
            }
        }

        #endregion
    }

    #endregion
}