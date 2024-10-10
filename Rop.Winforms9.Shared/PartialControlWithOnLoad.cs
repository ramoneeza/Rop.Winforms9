using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;
using Rop.IncludeFrom.Annotations;
using System.ComponentModel;

namespace Rop.Winforms9.Shared;

[DummyPartial]
internal partial class PartialControlWithOnLoad:Control,IControlWithOnLoad
{
    public event ControlEventHandler? FormLoad;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FormCreated { get; private set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FormLoaded { get; private set; }
    void IControlWithOnLoad.OnFormLoad(Form formcontrol)=> OnFormLoad(formcontrol);
    void IControlWithOnLoad.OnLayout(LayoutEventArgs levent)=>OnLayout(levent);
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        if (FormCreated) return;
        var p = Parent;
        while(p is not null and not Form)
        {
            p = p.Parent;
            if (p is ContainerControl ct) p=ct.ParentForm;
        }
        if (p is Form f2)
        {
            f2.Load += (sender,_)=>
            {
                FormLoaded= true;
                OnFormLoad(sender as Form);
            };
            FormCreated = true;
        }
    }
    protected virtual void OnFormLoad(Form? formcontrol)
    {
        FormLoad?.Invoke(this,new ControlEventArgs(formcontrol));
    }
}
