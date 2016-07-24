using G_coder.Model;

namespace G_coder.DxfConverter
{
    public interface IDxfConverter
    {
        void LoadFile(string pathToFile);

        Fields GetFields();

    }
}