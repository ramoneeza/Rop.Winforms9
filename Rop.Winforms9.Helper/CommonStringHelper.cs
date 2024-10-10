using System.Collections.Frozen;
using System.Globalization;

namespace Rop.Winforms9.Helper;

public static class CommonStringRepository
{
    private static readonly FrozenDictionary<string, LangRow> _names;
    static CommonStringRepository()
    {
        var names = new Dictionary<string,LangRow>();
        names.Add("OK", ("OK", "Aceptar"));
        names.Add("Cancel", ("Cancel", "Cancelar"));
        names.Add("Yes", ("Yes", "Sí"));
        names.Add("No", ("No", "No"));
        names.Add("Save", ("Save", "Guardar"));
        names.Add("Open", ("Open", "Abrir"));
        names.Add("New", ("New", "Nuevo"));
        names.Add("Exit", ("Exit", "Salir"));
        names.Add("Apply", ("Apply", "Aplicar"));
        names.Add("Help", ("Help", "Ayuda"));
        names.Add("About", ("About", "Acerca de"));
        names.Add("Undo", ("Undo", "Deshacer"));
        names.Add("Redo", ("Redo", "Rehacer"));
        names.Add("Cut", ("Cut", "Cortar"));
        names.Add("Copy", ("Copy", "Copiar"));
        names.Add("Paste", ("Paste", "Pegar"));
        _names = names.ToFrozenDictionary();
    }
    public static string Get(string name,int index)
    {
        if (index<0 || index >= LangRow.Count) index = 0; 
        if (_names.TryGetValue(name, out LangRow? value))
        {
            return value[index];
        }
        else
        {
            return name;
        }
    }
    public static string Get(string name,string lang)
    {
        return Get(name, LangRow.StringToIndex(lang));
    }
    public static int StringToIndex(string lang)=>LangRow.StringToIndex(lang);
    public static string IndexToString(int index) => LangRow.IndexToString(index);

    private record LangRow(string English, string Spanish)
    {
        public static implicit operator LangRow((string English, string Spanish) value) => new LangRow(value.English, value.Spanish);
        public string this[int i] => i switch
        {
            0 => English,
            1 => Spanish,
            _ => throw new IndexOutOfRangeException()
        };
        public static int Count => 2;
        public static int StringToIndex(string lang) => lang switch
        {
            "en" => 0,
            "es" => 1,
            _ => 0
        };
        public static string IndexToString(int index) => index switch
        {
            0 => "en",
            1 => "es",
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };
    }
}
public record CommonString
{
    public int Index { get; }
    public string Name { get; }
    public string Get(string name)
    {
        return CommonStringRepository.Get(name,Index);
    }
    public string this[string name] => Get(name);
        
    public CommonString(string name)
    {
        Name = name;
        Index = CommonStringRepository.StringToIndex(name);
    }
    public CommonString(int index)
    {
        Index = index;
        Name = CommonStringRepository.IndexToString(index);
    }
    public CommonString(CultureInfo culture):this(culture.ThreeLetterISOLanguageName){
    }
    public static CommonString Default { get; set; }= new CommonString(CultureInfo.CurrentCulture);
    public static string GetDefault(string name)=> Default.Get(name);
    
    public string StrOk=> Get("OK");
    public string StrCancel => Get("Cancel");
    public string StrYes => Get("Yes");
    public string StrNo => Get("No");
    public string StrApply => Get("Apply");
    public string StrSave => Get("Save");
    public string StrClose => Get("Close");
    public string StrHelp => Get("Help");
    public string StrOpen => Get("Open");
    public string StrNew => Get("New");
    public string StrExit => Get("Exit");
    public string StrAbout => Get("About");
    public string StrUndo => Get("Undo");
    public string StrRedo => Get("Redo");
    
}