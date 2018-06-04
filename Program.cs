using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace kurs

{

    class FEAL4//класс шифрования

    {

        static void Main(string[] args)//программа шифрования по методу Feal4

        {

            Console.WriteLine("Введите текст, который будет кодироваться:");

            //ввод текстовой строки

            string d = "";

            Add(ref d);//метод для ввода текста для шифрования

            byte[] x = Encoding.GetEncoding(1251).GetBytes(d);//перевод текста в последовательность байтов

            //Console.WriteLine("Textovaya");

            //Console.WriteLine(d);

            //string a = Console.ReadLine();

            //Console.WriteLine(a.Length);

            //byte[] x = Encoding.GetEncoding(1251).GetBytes(a);

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(x));

            byte[][] y = new byte[8][];//подготовка массива байтов для шифровки

            Console.WriteLine("Введите ключ для кодировки(ключ состоит из 8-ми символов):");//ввод ключа шифрования

            //ввод ключа

            Add(ref d);//метод для ввода ключа шифрования

            byte[] v = Encoding.GetEncoding(1251).GetBytes(d);//перевод ключа в последовательность байтов

            //byte[][] v = new byte[8] {Encoding.GetEncoding(1251).GetBytes(t)};

            //Console.WriteLine(x[1]);

            for (int i = 0; i < 6; i++)//цикл для обработки ключей шифрования

            {

                //y[i] = new byte[8] { 48, 49, 50, 51, 52, 53, 54, 55 };

                y[i] = v;

            }

            //Console.WriteLine("Kluch:");

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(y[0]));

            //вызов конструктора переменных real

            FEAL4 real = new FEAL4();

            //Вызов метода шифрования Encrypt

            byte[] s = real.Encrypt(x, y);

            //Вызов метода дешифрования Decrypt

            byte[] g = real.Decrypt(s, y);

            //Console.WriteLine(s);

        }

        //метод для считывания данных с клавиатуры для ввода текста

        private static void Add(ref string ask)//ref ask для возврата текста в main-часть

        {

            //объявление переменной для цикла проверки ввода значений

            bool one = true;

            while (one)//цикл проверки ввода значений

            {

                ask = Console.ReadLine();//ввод текста

                byte[] x = Encoding.GetEncoding(1251).GetBytes(ask);//перевод текста в последовательность байтов

                if (ask.Length == 8) one = false;

                else Console.WriteLine("Повторите ввод, точное кол-во сиволов - 8");

            }

        }

        //метод циклического сдвига влево на 2 или на 6 вправо

        private byte G0(byte a, byte b)

        {

            return (byte)((((a + b) % 256) << 2) | (((a + b) % 256) >> 6));//циклический сдвиг

        }

        //метод циклического сдвига влево на 2 или на 6 вправо

        private byte G1(byte a, byte b)

        {

            return (byte)((((a + b + 1) % 256) << 2) | (((a + b + 1) % 256) >> 6));//диклический сдвиг

        }

        //функция перестановки

        public byte[] F(byte[] x)//функция F получает 4 байта

        {

            byte[] y = new byte[4];//выделяется мессто под 4 байта

            //входные байты перемешиваются и проходят через функции G0 или G1

            //Функции G0 и G1 выполняют преобразование 16-битной входной последовательности в 8-битный результат.

            y[1] = G1((byte)(x[0] ^ x[1]), (byte)(x[2] ^ x[3]));

            y[0] = G0(x[0], y[1]);

            y[2] = G0(y[1], (byte)(x[2] ^ x[3]));

            y[3] = G1(y[2], x[3]);

            return y;

        }

        //Левый и правый блоки складываются по модулю два с 32-битными подключами K[4] и K[5] соответственно

        private void AddKeyPart(byte[] P, byte[] K)//на вход получаем левую или правую часть ключа и текста

        {

            for (int i = 0; i < 4; i++)

            {

                P[i] = (byte)(P[i] ^ K[i]);//побитовое

                //Console.WriteLine((K[i]));

                //Console.WriteLine("Error");

            }

        }

        //сложение по модулю два

        private byte[] XOR(byte[] a, byte[] b)

        {

            byte[] c = new byte[a.Length];//выделяем место под результат

            //Console.WriteLine((a));

            //Console.WriteLine((b));

            for (int i = 0; i < c.Length; i++)//побайтовое сложение

            {

                c[i] = (byte)(a[i] ^ b[i]);

            }

            return c;//возврат части шифра

        }

        //метод шифрования Feal4

        public byte[] Encrypt(byte[] P, byte[][] K)//получаем текст и массив ключей на вход

        {

            byte[] LeftPart = new byte[4];//выделяем 4 байта под левую часть текста

            byte[] RightPart = new byte[4];//выделяем 4 байта под правую часть текста

            Array.Copy(P, 0, LeftPart, 0, 4);//копируем левую часть текста

            Array.Copy(P, 4, RightPart, 0, 4);//копируем правую часть текста

            //Левый и правый блоки складываются по модулю два с 32-битными подключами K[4] и K[5] соответственно

            AddKeyPart(LeftPart, K[4]);//левая часть остается без изменений, а правая образуется сложением по модулю два с левым блоком.

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(LeftPart));

            //Console.WriteLine("неверно");

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(K[4]));

            AddKeyPart(RightPart, K[5]);//левая часть остается без изменений, а правая образуется сложением по модулю два с левым блоком.

            // Console.WriteLine("неверно1111");

            // Console.WriteLine(Encoding.GetEncoding(1251).GetString(K[5]));

            //Console.WriteLine("неверно2222");

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(RightPart));

            //Console.WriteLine("Верно");

            //левая часть остается без изменений, а правая образуется сложением по модулю два с левым блоком.

            byte[] Round2Left = XOR(LeftPart, RightPart);

            //выполняются 4 раунда шифрования

            //правый блок суммируется по модулю два с подключом раунда K[0], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round2Right = XOR(LeftPart, F(XOR(Round2Left, K[0])));

            //левый и правый блок меняют местами и полученный результат подается на вход следующего раунда

            byte[] Round3Left = Round2Right;

            //правый блок суммируется по модулю два с подключом раунда K[1], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round3Right = XOR(Round2Left, F(XOR(Round2Right, K[1])));

            //левый и правый блок меняют местами и полученный результат подается на вход следующего раунда

            byte[] Round4Left = Round3Right;

            //правый блок суммируется по модулю два с подключом раунда K[2], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round4Right = XOR(Round3Left, F(XOR(Round3Right, K[2])));

            //правый блок суммируется по модулю два с подключом раунда K[3], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] CipherTextLeft = XOR(Round4Left, F(XOR(Round4Right, K[3])));

            //правый блок складывается по модулю два с левым блоком и полученный результат возвращается в качестве правой части шифротекста. Левая же часть после 4 раунда остается неизменной и составляет первые 32 бита полученного шифротекста.

            byte[] CipherTextRight = XOR(Round4Right, CipherTextLeft);

            byte[] CipherText = new byte[8];//освобождаем память под 8-ми байтовую запись

            Array.Copy(CipherTextLeft, 0, CipherText, 0, 4);//копируем левую часть текста шифра в результирующую переменную

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(CipherText));

            Array.Copy(CipherTextRight, 0, CipherText, 4, 4);//копируем правую часть текста шифра в результирующую переменную

            Console.WriteLine("Зашифрованный текст:");

            Console.WriteLine(Encoding.GetEncoding(1251).GetString(CipherText));//печатаем зашифрованный текст

            return CipherText;//возвращаем байтовую последовательность в main

        }

        //метод расшифрования Feal4

        public byte[] Decrypt(byte[] P, byte[][] K)//получаем текст и массив ключей на вход

        {

            byte[] LeftPart = new byte[4];//выделяем 4 байта под левую часть зашифрованного текста

            byte[] RightPart = new byte[4];//выделяем 4 байта под правую часть зашифрованного текста

            Array.Copy(P, 0, LeftPart, 0, 4);//копируем левую часть зашифрованного текста

            Array.Copy(P, 4, RightPart, 0, 4);//копируем правую часть зашифрованного текста

            //выполняются 4 раунда дешифрования

            //правый блок складывается по модулю два с левым блоком и полученный результат возвращается в качестве правой части шифротекста. Левая же часть после 4 раунда остается неизменной и составляет первые 32 бита полученного шифротекста.

            byte[] Round4Right = XOR(LeftPart, RightPart);

            //правый блок суммируется по модулю два с подключом раунда K[3], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round4Left = XOR(LeftPart, F(XOR(Round4Right, K[3])));

            //левый и правый блок меняют местами и полученный результат подается на вход следующего раунда

            byte[] Round3Right = Round4Left;

            //правый блок суммируется по модулю два с подключом раунда K[2], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round3Left = XOR(Round4Right, F(XOR(Round3Right, K[2])));

            //левый и правый блок меняют местами и полученный результат подается на вход следующего раунда

            byte[] Round2Right = Round3Left;

            //правый блок суммируется по модулю два с подключом раунда K[1], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round2Left = XOR(Round3Right, F(XOR(Round2Right, K[1])));

            //левый и правый блок меняют местами и полученный результат подается на вход следующего раунда

            byte[] Round1Right = Round2Left;

            //правый блок суммируется по модулю два с подключом раунда K[0], а затем полученный результат прогоняется через функцию перестановки F.

            byte[] Round1Left = XOR(Round2Right, F(XOR(Round1Right, K[0])));

            //левой части текста присваиваем значение левого блока

            byte[] TextLeft = Round1Left;

            //правая часть текста образуется сложением по модулю два с левым блоком.

            byte[] TextRight = XOR(Round1Left, Round1Right);

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(TextLeft));

            //Console.WriteLine(Encoding.GetEncoding(1251).GetString(TextRight));

            AddKeyPart(TextLeft, K[4]);//левая часть остается без изменений, а правая образуется сложением по модулю два с левым блоком.

            AddKeyPart(TextRight, K[5]);//левая часть остается без изменений, а правая образуется сложением по модулю два с левым блоком.

            byte[] Text = new byte[8];//выделяем место под результат дешифровки

            Array.Copy(TextLeft, 0, Text, 0, 4);//копируем левую часть текста шифра в результирующую переменную

            Array.Copy(TextRight, 0, Text, 4, 4);//копируем правую часть текста шифра в результирующую переменную

            Console.WriteLine("Декодированный текст:");

            Console.WriteLine(Encoding.GetEncoding(1251).GetString(Text));//печатаем расшифрованный текст

            return Text;//возвращаем байтовую последовательность в main

        }

    }

}