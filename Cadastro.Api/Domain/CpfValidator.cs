namespace Cadastro.Api.Domain
{
    public static class CpfValidator
    {
        public static bool IsValid(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            string digits = OnlyDigits(cpf);

            if (digits.Length != 11)
                return false;

            // 111.111.111-11, 000.000.000-00 etc. são inválidos
            if (AllDigitsEqual(digits))
                return false;

            // converte cada caractere em número inteiro
            int[] numbers = new int[11];
            for (int i = 0; i < 11; i++)
            {
                numbers[i] = int.Parse(digits[i].ToString());
            }

            return numbers[9] == CalculateDigit(numbers, 9)
                && numbers[10] == CalculateDigit(numbers, 10);
        }

        // remove tudo que não for número (pontos, traço, espaços)
        public static string OnlyDigits(string text)
        {
            string result = "";
            foreach (char c in text)
            {
                if (c >= '0' && c <= '9')
                    result += c;
            }
            return result;
        }

        // compara cada dígito com o primeiro
        private static bool AllDigitsEqual(string digits)
        {
            for (int i = 1; i < digits.Length; i++)
            {
                if (digits[i] != digits[0])
                    return false;
            }
            return true;
        }

        // algoritmo módulo 11 da Receita Federal
        private static int CalculateDigit(int[] numbers, int length)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += numbers[i] * (length + 1 - i);
            }

            int rest = sum % 11;
            return rest < 2 ? 0 : 11 - rest;
        }
    }
}
