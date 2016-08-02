using G_coder.Model;

namespace G_coder.DxfConverter
{
    public interface IDxfConverter
    {
        void ConvertToFields(string pathToFile);

        Fields GetFields();

    }
}