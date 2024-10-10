namespace Rop.Winforms9.KeyValueListComboBox
{
    public interface IKeyValueControl:IKeyValueColors
    {
        int KeyWidth { get;  }
        int CenterMargin { get;  }
        int TopMargin { get; }
        int LeftIconSpaces { get;  }
        int RightIconSpaces { get;}
        object? GetItem(int item);
    }
}