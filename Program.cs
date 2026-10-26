
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace caso_semana11
{
    internal class Program
    {
        static int max_estudiantes = 40;
        static int max_cursos = 4;
        static string[] nombre_de_cursos = { "Matematicas", "programacion", "base de datos", "fisica" };
        static string[] estudiantes = new string[max_estudiantes];
        static double[,] matrizNotas = new double[max_estudiantes, max_cursos];
        static int total_estudiantes = 0;
        static string rutaArchivo = "notas-cursos.txt";
        public static void RegistrarEstudiante()
        {
            Console.WriteLine(" registro de estudiante");
            if (total_estudiantes>= max_estudiantes)
            {
                Console.WriteLine("capacidad maxima de estudiantes alcanzada");
                return;
            }
            string nombre = validador.Leertexto("ingresar nombres y apellidos").ToUpper();
            estudiantes[total_estudiantes] = nombre;
            Console.WriteLine("ingresar  notas para cada para cada curso (0 a 20");
            for(int i = 0; i < max_cursos; i++)
            {
                matrizNotas[total_estudiantes, i] = validador.LeerDecimal($"-{nombre_de_cursos[i]}:");
            }
            total_estudiantes++;
            Console.WriteLine("estudiante registro...!");
        }
        public static void guardadrArchivos()
        {
            Console.Write("guardar Archivo");
            if(total_estudiantes == 0)
            {
                Console.WriteLine("no hay registros por guardar");
                return;
            }
            using (StreamWriter escribir=new StreamWriter(rutaArchivo))
            {
                for(int i = 0; i < total_estudiantes; i++)
                {
                    string linea = estudiantes[i];
                    for(int j = 0; j < max_cursos; j++)
                    {
                        linea += $",{matrizNotas[i, j]}";
                        //linea += "," + matrizNotas[i, j];
                    }
                    escribir.WriteLine(linea);
                }
            }
            Console.WriteLine("registros guardados");
        }
        public static void CargarArchivos()
        {
            Console.WriteLine("cargar registros desde archivo");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("no encontro archivo..");
                return;
            }
            total_estudiantes = 0;
            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null && total_estudiantes < max_estudiantes)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == max_cursos + 1)
                    {
                        estudiantes[total_estudiantes] = partes[0];
                        for (int i = 0; i < max_cursos; i++)
                        {
                            matrizNotas[total_estudiantes, i] = Convert.ToDouble(partes[i + 1]);
                        }
                        total_estudiantes++;
                    }
                }
            }
            Console.WriteLine("se cargaron" + total_estudiantes + "con sus notas");
        }
        public static void mostrarResgistros()
        {
            Console.WriteLine(new string('=' , 100));
            Console.WriteLine("\t\tReporte general de notas por cursos");
            Console.WriteLine(new string('=', 100));
            if (total_estudiantes == 0)
            {
                Console.WriteLine("no hay registros");
                return;
            }

            Console.WriteLine($"{"estudiante",-25} |");
            for(int i = 0; i < max_cursos; i++)
            {
                Console.WriteLine($"{nombre_de_cursos[i],-12}|");
            }
            Console.WriteLine("promedio");
            Console.WriteLine(new string('-', 100));
            for(int i = 0; i < total_estudiantes; i++)
            {
                Console.WriteLine($"{estudiantes[i],-25}|");
                double suma = 0;
                for (int j = 0; j < max_cursos; j++)
                {
                    Console.Write($"{matrizNotas[i, j],-12:F1}");
                    suma += matrizNotas[i, j];
                }
                double promedio = suma / max_cursos;
                string estado = promedio >= 12 ? "aprobado" : "desaprobado";
                Console.WriteLine($"{promedio:F2}({estado})");
            }
        }
        static void Main(string[] args)
        {
            int opc = 0; 
            while(opc != 5)
            {
                Console.Clear();
                Console.WriteLine("[1]registrar estudiante");
                Console.WriteLine("[2]guardar el archivo");
                Console.WriteLine("[3]cargar el archivo");
                Console.WriteLine("[4]mostrar los registros");
                Console.WriteLine("[5]salir");
                opc = validador.LeerEnteros("selecciona la opcion[1-5]: ");
                switch (opc)
                {
                    case 1:
                        RegistrarEstudiante(); break;
                    case 2:
                        guardadrArchivos(); break;
                    case 3:
                        CargarArchivos(); break;
                    case 4:
                        mostrarResgistros(); break;
                    case 5:
                        Console.WriteLine("muchas exitos para yu examen final"); break;
                }
                Console.ReadKey();
            }
        }
    }
}
