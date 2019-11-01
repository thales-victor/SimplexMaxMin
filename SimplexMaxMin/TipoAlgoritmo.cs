namespace SimplexMaxMin
{
    public class TipoAlgoritmo
    {
        public static int Maximizacao = 0;
        public static int Minimizacao = 1;

        public string GetDescricao(int tipo)
        {
            if (tipo == Maximizacao)
                return "Mazimização";

            if (tipo == Minimizacao)
                return "Minimização";

            return "";
        }
    }
}
