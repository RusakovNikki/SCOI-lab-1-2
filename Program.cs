using System;
using System.Diagnostics;
using System.IO;



/* для подключения System.Drawing в своем проекте правой в проекте нажать правой кнопкой по Ссылкам -> Добавить ссылку
    отметить галочкой сборку System.Drawing    */
using System.Drawing;
using System.Drawing.Drawing2D;


namespace IMGapp
{

    class Program
    {

        static int product(int Px, int Py, int Ax, int Ay, int Bx, int By)
        {
            return (Bx - Ax) * (Py - Ay) - (By - Ay) * (Px - Ax);
        }
        static void Main(string[] args)
        {

            Console.WriteLine("Введите название первого файла");
            var file = Console.ReadLine();
            file = @"..\\..\\" + file + ".jpg";
            FileInfo fi = new FileInfo(file);
            while(!fi.Exists) {
                Console.WriteLine("Такого файла нет!!! Введите название файла");
                file = Console.ReadLine();
                file = @"..\\..\\" + file + ".jpg";
                fi = new FileInfo(file);
            }
            Console.WriteLine("Введите название второго файла");
            var file2 = Console.ReadLine();
            file2 = @"..\\..\\" + file2 + ".jpg";
            FileInfo fi2 = new FileInfo(file2);
            while (!fi2.Exists)
            {
                Console.WriteLine("Такого файла нет!!! Введите название файла");
                file2 = Console.ReadLine();
                file2 = @"..\\..\\" + file2 + ".jpg";
                fi2 = new FileInfo(file2);
            }

            Console.WriteLine("Что необходимо сделать?");
            Console.WriteLine("1. Попиксельная сумма\n2. Произведение\n3. Среднее арифметическое\n4. Минимум\n5. Максимум\n6. Наложение маски\n7. Градационное преобразование");
            var choice = int.Parse(Console.ReadLine());
            using (var img = new Bitmap(file))    //открываем картинку     
            {     //блок using используется для корретного высвобождения памяти переменной, которая в нем создается
                    //для типа Bitmap это необходимо.
                    //вне блока using объект, в нем созданный, будет уже не доступен.
                    //Внутри этого блока using нельзя будет сохранить новое изображение в файл in.jpg,
                    //т.к. пока загруженный битмап висит в памяти файл открыт.

                //Console.WriteLine("Открываю изображение " + Directory.GetParent("..\\..\\") + file);


                var w = img.Width;
                var h = img.Height;
                using (var img2 = new Bitmap(file2))
                {
                    using (var img_out = new Bitmap(w, h))   //создаем пустое изображение размером с исходное для сохранения результата
                    {
                        var time1 = DateTime.Now;
                        Stopwatch timer = new Stopwatch();
                        timer.Start();

                        //попиксельно обрабатываем картинку 
                        for (int i = 0; i < h; ++i)
                        {
                            for (int j = 0; j < w; ++j)
                            {
                                if (choice == 1)
                                {
                                    //считывыем пиксель картинки и получаем его цвет
                                    var pix = img.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r = pix.R;
                                    int g = pix.G;
                                    int b = pix.B;

                                    var pix2 = img2.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r2 = pix2.R;
                                    int g2 = pix2.G;
                                    int b2 = pix2.B;

                                    //Увеличим квет каждого пикселя на 1.4
                                    //При вычислении пикселей используем функию Clamp (см. ниже Main) чтобы цвет не вылезал за границы [0 255]
                                    r = (int)Clamp(r + r2, 0, 255);
                                    g = (int)Clamp(g + g2, 0, 255);
                                    b = (int)Clamp(b + b2, 0, 255);


                                    //записываем пиксель в изображение
                                    pix = Color.FromArgb(r, g, b);
                                    img_out.SetPixel(j, i, pix);
                                    //ц-ции GetPixel и SetPixel работают достаточно медленно, надо стримится к минимизации их использования
                                }
                                if (choice == 2)
                                {
                                    //считывыем пиксель картинки и получаем его цвет
                                    var pix = img.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r = pix.R;
                                    int g = pix.G;
                                    int b = pix.B;

                                    var pix2 = img2.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r2 = pix2.R;
                                    int g2 = pix2.G;
                                    int b2 = pix2.B;

                                    //Увеличим квет каждого пикселя на 1.4
                                    //При вычислении пикселей используем функию Clamp (см. ниже Main) чтобы цвет не вылезал за границы [0 255]
                                    r = (int)Clamp((r * r2) / 255, 0, 255);
                                    g = (int)Clamp((g * g2) / 255, 0, 255);
                                    b = (int)Clamp((b * b2) / 255, 0, 255);


                                    //записываем пиксель в изображение
                                    pix = Color.FromArgb(r, g, b);
                                    img_out.SetPixel(j, i, pix);
                                    //ц-ции GetPixel и SetPixel работают достаточно медленно, надо стримится к минимизации их использования
                                }
                                if (choice == 3)
                                {
                                    //считывыем пиксель картинки и получаем его цвет
                                    var pix = img.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r = pix.R;
                                    int g = pix.G;
                                    int b = pix.B;

                                    var pix2 = img2.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r2 = pix2.R;
                                    int g2 = pix2.G;
                                    int b2 = pix2.B;

                                    //Увеличим квет каждого пикселя на 1.4
                                    //При вычислении пикселей используем функию Clamp (см. ниже Main) чтобы цвет не вылезал за границы [0 255]
                                    r = (int)Clamp((r + r2) / 2, 0, 255);
                                    g = (int)Clamp((g + g2) / 2, 0, 255);
                                    b = (int)Clamp((b + b2) / 2, 0, 255);


                                    //записываем пиксель в изображение
                                    pix = Color.FromArgb(r, g, b);
                                    img_out.SetPixel(j, i, pix);
                                    //ц-ции GetPixel и SetPixel работают достаточно медленно, надо стримится к минимизации их использования
                                }
                                if (choice == 4)
                                {
                                    //считывыем пиксель картинки и получаем его цвет
                                    var pix = img.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r = pix.R;
                                    int g = pix.G;
                                    int b = pix.B;

                                    var pix2 = img2.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r2 = pix2.R;
                                    int g2 = pix2.G;
                                    int b2 = pix2.B;

                                    //Увеличим квет каждого пикселя на 1.4
                                    //При вычислении пикселей используем функию Clamp (см. ниже Main) чтобы цвет не вылезал за границы [0 255]
                                    r = (int)Clamp((r < r2) ? r : r2, 0, 255);
                                    g = (int)Clamp((g < g2) ? g : g2, 0, 255);
                                    b = (int)Clamp((b < b2) ? b : b2, 0, 255);


                                    //записываем пиксель в изображение
                                    pix = Color.FromArgb(r, g, b);
                                    img_out.SetPixel(j, i, pix);
                                    //ц-ции GetPixel и SetPixel работают достаточно медленно, надо стримится к минимизации их использования
                                }
                                if (choice == 5)
                                {
                                    //считывыем пиксель картинки и получаем его цвет
                                    var pix = img.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r = pix.R;
                                    int g = pix.G;
                                    int b = pix.B;

                                    var pix2 = img2.GetPixel(j, i);

                                    //получаем цветовые компоненты цвета
                                    int r2 = pix2.R;
                                    int g2 = pix2.G;
                                    int b2 = pix2.B;

                                    //Увеличим квет каждого пикселя на 1.4
                                    //При вычислении пикселей используем функию Clamp (см. ниже Main) чтобы цвет не вылезал за границы [0 255]
                                    r = (int)Clamp((r > r2) ? r : r2, 0, 255);
                                    g = (int)Clamp((g > g2) ? g : g2, 0, 255);
                                    b = (int)Clamp((b > b2) ? b : b2, 0, 255);


                                    //записываем пиксель в изображение
                                    pix = Color.FromArgb(r, g, b);
                                    img_out.SetPixel(j, i, pix);
                                    //ц-ции GetPixel и SetPixel работают достаточно медленно, надо стримится к минимизации их использования
                                }
                                
                            }
                        }
                        if (choice == 6)
                        {         //Graphics как раз содержит методы для рисования линий, текста и прочих геомиетричсеких примитивов
                            var width = img.Width;
                            var height = img.Height;
                            
                              Console.WriteLine("На какой файл наложить маску?\n1." + file + "\n2. " + file2);
                            var choiceMask = int.Parse(Console.ReadLine());
                            Console.WriteLine("Выберите тип маски:\n1. Круг\n2. Квадрат\n3. Прямоугольник");
                            var choiceFigure = int.Parse(Console.ReadLine());

                            if (choiceFigure == 1)
                            {
                                Console.WriteLine("Введите радиус круга");
                                var radiusSercle = int.Parse(Console.ReadLine());
                                Console.WriteLine("Введите Координаты центра круга");
                                var coordCircleX = int.Parse(Console.ReadLine());
                                var coordCircleY = int.Parse(Console.ReadLine());

                                for (int i = 0; i < h; ++i)
                                {
                                    for (int j = 0; j < w; ++j)
                                    {
                                        if (choiceMask == 1)
                                        {
                                            var pix1 = img.GetPixel(j, i);
                                            int r = pix1.R;
                                            int g = pix1.G;
                                            int b = pix1.B;
                                            if (Math.Pow((i - coordCircleX), 2) + Math.Pow((j - coordCircleY), 2) > radiusSercle * radiusSercle)
                                            {
                                                r = 255;
                                                g = 255;
                                                b = 255;
                                            }
                                            pix1 = Color.FromArgb(r, g, b);
                                            img_out.SetPixel(j, i, pix1);
                                        }
                                        if (choiceMask == 2)
                                        {
                                            var pix1 = img2.GetPixel(j, i);
                                            int r = pix1.R;
                                            int g = pix1.G;
                                            int b = pix1.B;
                                            if (Math.Pow((i - coordCircleX), 2) + Math.Pow((j - coordCircleY), 2) > radiusSercle * radiusSercle)
                                            {
                                                r = 255;
                                                g = 255;
                                                b = 255;
                                            }
                                            pix1 = Color.FromArgb(r, g, b);
                                            img_out.SetPixel(j, i, pix1);
                                        }
                                    }
                                }
                            }

                            if (choiceFigure == 2)
                            {
                                Console.WriteLine("Введите сторону квадрата");
                                var SquareSide = int.Parse(Console.ReadLine());
                                Console.WriteLine("Введите Координаты центра круга");
                                var coordSquareX = int.Parse(Console.ReadLine());
                                var coordSquareY = int.Parse(Console.ReadLine());

                                int x1, x2, x3, x4;
                                int y1, y2, y3, y4;
                                y1 = y2 = coordSquareY + SquareSide / 2; // y1y4
                                y3 = y4 = coordSquareY - SquareSide / 2; // y2 y3
                                x1 = x4 = coordSquareX - SquareSide / 2; // x1 x2
                                x2 = x3 = coordSquareX + SquareSide / 2; // x3 x4

                                for (int i = 0; i < h; ++i)
                                {
                                    for (int j = 0; j < w; ++j)
                                    {
                                        int p1 = product(j, i, x1, y1, x2, y2),
                                            p2 = product(j, i, x2, y2, x3, y3),
                                            p3 = product(j, i, x3, y3, x4, y4),
                                            p4 = product(j, i, x4, y4, x1, y1);
                                        if (choiceMask == 1)
                                        {
                                            var pix1 = img.GetPixel(j, i);
                                            int r = pix1.R;
                                            int g = pix1.G;
                                            int b = pix1.B;
                                            if ((p1 < 0 && p2 < 0 && p3 < 0 && p4 < 0) ||
                                                (p1 > 0 && p2 > 0 && p3 > 0 && p4 > 0))
                                            {
                                            }
                                            else
                                            {
                                                r = 255;
                                                g = 255;
                                                b = 255;
                                            }
                                            pix1 = Color.FromArgb(r, g, b);
                                            img_out.SetPixel(j, i, pix1);
                                        }
                                        if (choiceMask == 2)
                                        {
                                            var pix1 = img2.GetPixel(j, i);
                                            int r = pix1.R;
                                            int g = pix1.G;
                                            int b = pix1.B;
                                            if ((p1 < 0 && p2 < 0 && p3 < 0 && p4 < 0) ||
                                                (p1 > 0 && p2 > 0 && p3 > 0 && p4 > 0))
                                            {
                                            }
                                            else
                                            {
                                                r = 255;
                                                g = 255;
                                                b = 255;
                                            }
                                            pix1 = Color.FromArgb(r, g, b);
                                            img_out.SetPixel(j, i, pix1);
                                        }
                                    }
                                }
                            }
                            if (choiceFigure == 3)
                            {
                                Console.WriteLine("Введите стороны прямоугольника");
                                var rectangleSide1 = int.Parse(Console.ReadLine());
                                var rectangleSide2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Введите Координаты центра круга");
                                var coordSquareX = int.Parse(Console.ReadLine());
                                var coordSquareY = int.Parse(Console.ReadLine());

                                int x1, x2, x3, x4;
                                int y1, y2, y3, y4;
                                y1 = y2 = coordSquareY + rectangleSide1 / 2; // y1y4
                                y3 = y4 = coordSquareY - rectangleSide1 / 2; // y2 y3
                                x1 = x4 = coordSquareX - rectangleSide2 / 2; // x1 x2
                                x2 = x3 = coordSquareX + rectangleSide2 / 2; // x3 x4

                                for (int i = 0; i < h; ++i)
                                {
                                    for (int j = 0; j < w; ++j)
                                    {
                                        int p1 = product(j, i, x1, y1, x2, y2),
                                            p2 = product(j, i, x2, y2, x3, y3),
                                            p3 = product(j, i, x3, y3, x4, y4),
                                            p4 = product(j, i, x4, y4, x1, y1);
                                        if (choiceMask == 2)
                                        {
                                            var pix1 = img2.GetPixel(j, i);
                                            int r = pix1.R;
                                            int g = pix1.G;
                                            int b = pix1.B;
                                            if ((p1 < 0 && p2 < 0 && p3 < 0 && p4 < 0) ||
                                                (p1 > 0 && p2 > 0 && p3 > 0 && p4 > 0))
                                            {
                                            }
                                            else
                                            {
                                                r = 255;
                                                g = 255;
                                                b = 255;
                                            }
                                            pix1 = Color.FromArgb(r, g, b);
                                            img_out.SetPixel(j, i, pix1);
                                        }
                                    }
                                }
                            }

                        }
                        if (choice == 7)
                        {
                            using (var img_out_gist = new Bitmap(256, 500)) {
                                Console.WriteLine("На какой файл наложить эффект?\n1." + file + "\n2. " + file2);
                                var choiceMask = int.Parse(Console.ReadLine());
                                ////////////
                                Console.WriteLine("Введите количество точек на графике");
                                var countPoint = int.Parse(Console.ReadLine());
                                int[] arrayPointX = new int[countPoint];
                                int[] arrayPointY = new int[countPoint];
                                for (int i = 0; i < countPoint; i++)
                                {
                                    Console.WriteLine("Введите х" + i + "-ой точки в пределах от 0 до 256");
                                    arrayPointX[i] = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Введите y" + i + "-ой точки в пределах от 0 до 256");
                                    arrayPointY[i] = int.Parse(Console.ReadLine());
                                }
                                int position = 0;
                                var coordStopX = arrayPointX[position];
                                var coordStopY = arrayPointY[position];
                                int coordStopXLast = 0;
                                int coordStopYLast = 0;


                                var breakWhile = false;
                                var isTrue = false;
                                while (!breakWhile)
                                {
                                    for (int i = 0; i < h; i++)
                                    {
                                        for (int j = 0; j < w; j++)
                                        {


                                            var pix = img.GetPixel(j, i);
                                            //получаем цветовые компоненты цвета
                                            int r = pix.R;
                                            int g = pix.G;
                                            int b = pix.B;
                                            var pix2 = img2.GetPixel(j, i);
                                            //получаем цветовые компоненты цвета
                                            int r2 = pix.R;
                                            int g2 = pix.G;
                                            int b2 = pix.B;


                                            //Увеличим квет каждого пикселя на 1.4
                                            //При вычислении пикселей используем функию Clamp (см. ниже Main) чтобы цвет не вылезал за границы [0 255]

                                            /*if (choiceMask == 1)
                                            {
                                                if (r < coordStopY && r > coordStopYLast || r < coordStopX && r > coordStopXLast || r > coordStopX && r < coordStopXLast || r > coordStopY && r < coordStopYLast)
                                                {
                                                    r = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (r - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                }
                                                if (g < coordStopY && g > coordStopYLast || g < coordStopX && g > coordStopXLast || g > coordStopX && g < coordStopXLast || g > coordStopY && g < coordStopYLast)
                                                {
                                                    g = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (g - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                }
                                                if (b < coordStopY && b > coordStopYLast || b < coordStopX && b > coordStopXLast || b > coordStopX && b < coordStopXLast || b > coordStopY && b < coordStopYLast)
                                                {
                                                    b = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (b - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                }
                                            }*/
                                            if (choiceMask == 1)
                                            {
                                                r = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (r - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                g = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (g - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                b = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (b - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                pix = Color.FromArgb(r, g, b);
                                                img_out.SetPixel(j, i, pix);
                                            }
                                            if (choiceMask == 2)
                                            {
                                                r2 = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (r2 - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                g2 = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (g2 - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                b2 = (int)Clamp((coordStopY + (coordStopYLast - coordStopY) * (b2 - coordStopX) / (coordStopXLast - coordStopX)), 0, 255);
                                                pix2 = Color.FromArgb(r2, g2, b2);
                                                img_out.SetPixel(j, i, pix2);
                                            }

                                        }
                                    }
                                    if (isTrue)
                                    {
                                        breakWhile = true;
                                    }
                                    if (position < arrayPointX.Length - 1)
                                    {
                                        position++;
                                        coordStopXLast = coordStopX;
                                        coordStopYLast = coordStopY;
                                        coordStopX = arrayPointX[position];
                                        coordStopY = arrayPointY[position];
                                        continue;
                                    }
                                    
                                    if (position == arrayPointX.Length - 1)
                                    {
                                        coordStopXLast = coordStopX;
                                        coordStopYLast = coordStopY;
                                        coordStopX = w;
                                        coordStopY = h;
                                        breakWhile = true;
                                    }
                                }

                                ////////////гистограмма////////////
                                var width = img_out_gist.Width;
                                var height = img_out_gist.Height;
                                int[] heightOfColor = new int[256];
                                int color;
                                int maxColor = 0;

                                for (int i = 0; i < h; i++)
                                {
                                    for (int j = 0; j < w; j++)
                                    {
                                        color = (img_out.GetPixel(j, i).R + img_out.GetPixel(j, i).G + img_out.GetPixel(j, i).B) / 3;
                                        heightOfColor[color]++;
                                        if (color > maxColor) maxColor = color;
                                    }
                                }
                                var k = img_out_gist.Height / maxColor;
                                for (int i = 0; i < 255; i++)
                                {
                                    var gr = Graphics.FromImage(img_out_gist);
                                    Pen p = new Pen(Color.Gray, 1);// цвет линии и ширина
                                    Point p1 = new Point(i, img_out_gist.Height - 1);// первая точка
                                    Point p2 = new Point(i, img_out_gist.Height - 1 - heightOfColor[i] / maxColor);// вторая точка
                                    gr.DrawLine(p, p1, p2);// рисуем линию
                                    gr.Dispose();
                                }
                                img_out_gist.Save("..\\..\\" + "гистограмма" + ".jpg");
                            }

                        }



                        timer.Stop();

                        Console.WriteLine("Обработал изображение за " + timer.ElapsedMilliseconds + " мс.");
                        Console.WriteLine("Как назовём файл?");
                        var image_out = Console.ReadLine();
                        //сохраним нашу выходную картинку 
                        img_out.Save("..\\..\\" + image_out + ".jpg");


                        Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + image_out + ".jpg");
                        Console.ReadKey();

                    }
                }//using (var img_out = new Bitmap(w, h))     вот тут картинка img_out удаляется методом img_out.Dispose()     

            }
            
             //using (var img = new Bitmap("in.jpg"))   вот тут картинка img удаляется методом img.Dispose()     

             //static void Main(string[] args)
        }

            

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }


}
