using System;

namespace BattleshipGame
{
    class Program {


        static void Main(string[] args) {

            // Console.ForegroundColor = ConsoleColor.Blue; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Green; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.White; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Black; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkBlue; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("~" + " "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("~" + " "); Console.ResetColor();

            Juego juego = new Juego(); //  ║ ╣ ╗  ╝ ═ ╠ ╦ ╩ ╔ ╚ ▀ █ ≡ ¤ ■ # ~
            juego.Empezar();

        }

    }

    class Juego {

        private readonly int tamaño_tablero = 10;
        private char[,] usuario_tablero;
        private char[,] computadora_tablero;
        private readonly int[] barcos_dimensiones = { 5, 4, 3, 2 };
        private readonly char casilla_vacia = ' ';
        private readonly char casilla_agua = 'O';
        private readonly char casilla_tocado = 'X';
        private readonly char casilla_hundido = '#';

        public void Empezar() {

            inicializar_tableros();
            colocar_barcos();
            jugar();

        }

        private void inicializar_tableros() {

            usuario_tablero = new char[tamaño_tablero, tamaño_tablero];
            computadora_tablero = new char[tamaño_tablero, tamaño_tablero];

            for (int i = 0; i < tamaño_tablero; i++) {

                for (int j = 0; j < tamaño_tablero; j++) {

                    usuario_tablero[i, j] = casilla_vacia;
                    computadora_tablero[i, j] = casilla_vacia;

                }

            }

        }

        private void colocar_barcos() { 

            Console.WriteLine("\n\t" + "Jugador, coloca tus barcos: ");
            colocar_barcos_usuario();
            colocar_barcos_computadora();

        }



        private void colocar_barcos_usuario() {

            foreach (int tamaño in barcos_dimensiones) {

                if (tamaño == 5) {
                    mostrar_tablero(usuario_tablero);
                    Console.WriteLine("\n\n\t" + $"Coloca tu portaaviones ({tamaño} casillas): ");
                }
                else if (tamaño == 4) { Console.WriteLine("\n\n\t" + $"Coloca tu acorazado ({tamaño} casillas): "); }
                else if (tamaño == 3) { Console.WriteLine("\n\n\t" + $"Coloca tu submarino ({tamaño} casillas): "); }
                else { Console.WriteLine("\n\n\t" + $"Coloca tu destructor ({tamaño} casillas): "); }


                int fila = leer_coordenada("fila");
                int columna = leer_coordenada("columna");

                char orientacion = leer_orientacion();

                colocar_barco(usuario_tablero, tamaño, fila, columna, orientacion);
                mostrar_tablero(usuario_tablero);

            }

        }

        private int leer_coordenada(string tipo_coordenada) {

            int coordenada;

            while (true) {

                Console.WriteLine("\n\t" + $"Introduce la {tipo_coordenada} del barco (0-9): ");

                Console.Write("\n\t" + ">> ");

                if (int.TryParse(Console.ReadLine(), out coordenada) && coordenada >= 0 && coordenada <= tamaño_tablero) {

                    break;
                }

                Console.WriteLine("\n\t" + "Coordenada inválida. Prueba de nuevo.");

            }

            return coordenada;

        }

        private char leer_orientacion() {

            char orientacion;

            Console.WriteLine("\n\t" + "Introduce la orientación del barco (H para horizontal, V para vertical): ");

            Console.Write("\n\t" + ">> ");

            orientacion = Console.ReadLine().ToUpper()[0];

            while (orientacion != 'H' && orientacion != 'V') {

                Console.WriteLine("\n\t" + "Coordenada inválida. Prueba de nuevo.");
                Console.WriteLine("\n\t" + "Introduce la orientación del barco (H para horizontal, V para vertical): ");
                Console.Write("\n\t" + ">> ");
                orientacion = Console.ReadLine().ToUpper()[0];

            }

            return orientacion;

        }

        private bool validar_posicion(char[,] tablero, int tamaño, int fila, int columna, char orientacion) {

            if (orientacion == 'H') {

                for (int i = columna; i < columna + tamaño; i++) {

                    if (i >= tamaño_tablero || tablero[fila, i] != casilla_vacia) {

                        return false;

                    }

                }

                return true;

            } else {

                for (int i = fila; i < fila + tamaño; i++) {

                    if (i >= tamaño_tablero || tablero[i, columna] != casilla_vacia) {

                        return false;

                    }

                }

                return true;

            }

        }

        private void colocar_barco(char[,] tablero, int tamaño, int fila, int columna, char orientacion) {

            while (!validar_posicion(tablero, tamaño, fila, columna, orientacion)) {

                if (orientacion == 'H') {

                    columna--;

                } else {

                    fila--;

                }

            }

            if (orientacion == 'H') {

                for (int i = columna; i < columna + tamaño; i++) {

                    tablero[fila, i] = 'S';

                }

            } else {

                for (int i = fila; i < fila + tamaño; i++) {

                    tablero[i, columna] = 'S';

                }

            }

        }


        private void colocar_barcos_computadora() {

            Random aleatorio = new Random();

            foreach (int size in barcos_dimensiones) {

                int fila;
                int columna;
                char orientacion;

                do {

                    fila = aleatorio.Next(tamaño_tablero);
                    columna = aleatorio.Next(tamaño_tablero);
                    orientacion = aleatorio.Next(2) == 0 ? 'H' : 'V';

                } while (!poder_colocar_barco(computadora_tablero, size, fila, columna, orientacion));

                colocar_barco(computadora_tablero, size, fila, columna, orientacion);

            }

        }

        private bool poder_colocar_barco(char[,] tablero, int tamaño, int fila, int columna, char orientacion) {

            if (orientacion == 'H') {

                if (columna + tamaño > tamaño_tablero) {

                    return false;

                }

                for (int i = columna; i < columna + tamaño; i++) {

                    if (tablero[fila, i] != casilla_vacia) {

                        return false;

                    }

                }

                for (int i = columna - 1; i <= columna + tamaño; i++) {

                    for (int j = fila - 1; j <= fila + 1; j++) {

                        if (i >= 0 && i < tamaño_tablero && j >= 0 && j < tamaño_tablero && tablero[j, i] != casilla_vacia) {

                            return false;

                        }

                    }

                }

            } else {

                if (fila + tamaño > tamaño_tablero) {

                    return false;

                }

                for (int i = fila; i < fila + tamaño; i++) {

                    if (tablero[i, columna] != casilla_vacia) {

                        return false;

                    }

                }

                for (int i = fila - 1; i <= fila + tamaño; i++) {

                    for (int j = columna - 1; j <= columna + 1; j++) {

                        if (i >= 0 && i < tamaño_tablero && j >= 0 && j < tamaño_tablero && tablero[i, j] != casilla_vacia) {

                            return false;

                        }

                    }

                }

            }

            return true;

        }

        private void mostrar_tablero(char[,] board) {

            Console.WriteLine("\n\t" + "  ╔═════════════════════╗");

            for (int i = 0; i < tamaño_tablero; i++) {

                Console.Write("\t" + i + " ║ ");

                for (int j = 0; j < tamaño_tablero; j++) {

                    Thread.Sleep(10);

                    if (board[i, j].Equals('O')) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("~" + " "); Console.ResetColor(); }

                    else if (board[i, j].Equals('X')) { Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("■" + " "); Console.ResetColor(); }

                    else if (board[i, j].Equals('S')) { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("■" + " "); Console.ResetColor(); }

                    else { Console.Write(board[i, j] + " "); }

                }

                Console.WriteLine("║");

            }

            Console.Write("\t" + "  ╚═════════════════════╝");
            Console.Write("\n\t" + "    0 1 2 3 4 5 6 7 8 9");

        }

        private void mostrar_ambos_tableros() {

            Console.WriteLine("\n\t" + "     Tablero oponente" + "\t" + "\t" + "\t" + "\t" + "        Tu tablero");
            Console.WriteLine("\n\t" + "  ╔═════════════════════╗" + "\t" + "\t" + "\t" + "  ╔═════════════════════╗");

            for (int i = 0; i < computadora_tablero.GetLength(0); i++) {

                Console.Write("\t" + i + " ║ ");

                for (int j = 0; j < computadora_tablero.GetLength(1); j++) {

                    if (computadora_tablero[i, j].Equals('O')) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("~" + " "); Console.ResetColor(); }

                    else if (computadora_tablero[i, j].Equals('X')) { Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("■" + " "); Console.ResetColor(); }

                    else if (computadora_tablero[i, j].Equals('S')) { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(" " + " "); Console.ResetColor(); }

                    else if (usuario_tablero[i, j].Equals('#')) { Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("■" + " "); Console.ResetColor(); }

                    else { Console.Write(computadora_tablero[i, j] + " "); }
                }

                Console.Write("║");

                Console.Write("\t\t");

                Console.Write("\t" + i + " ║ ");

                for (int j = 0; j < usuario_tablero.GetLength(1); j++) {

                    if (usuario_tablero[i, j].Equals('O')) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("~" + " "); Console.ResetColor(); }

                    else if (usuario_tablero[i, j].Equals('X')) { Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("■" + " "); Console.ResetColor(); }

                    else if (usuario_tablero[i, j].Equals('S')) { Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("■" + " "); Console.ResetColor(); }

                    else if (usuario_tablero[i, j].Equals('#')) { Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("■" + " "); Console.ResetColor(); }

                    else { Console.Write(usuario_tablero[i, j] + " "); }

                }

                Console.WriteLine("║");

            }

            Console.Write("\t" + "  ╚═════════════════════╝" + "\t" + "\t" + "\t" + "  ╚═════════════════════╝");
            Console.Write("\n\t" + "    0 1 2 3 4 5 6 7 8 9" + "\t" + "\t" + "\t" + "\t" + "    0 1 2 3 4 5 6 7 8 9");

        }


        private void jugar() {

            Console.WriteLine("\n\n\t" + "El juego ha empezado. Buena suerte!");

            mostrar_ambos_tableros();

            while (true) {

                Console.WriteLine("\n\n\t" + "Es tu turno. Introduce las coordenadas para el ataque: ");
                int row = leer_coordenada("fila");
                int col = leer_coordenada("columna");

                ataque(computadora_tablero, row, col);

                // mostrar_tablero(computadora_tablero);

                if (fin_partida(computadora_tablero)) {

                    Console.WriteLine("\n\n\n\t" + "     Tablero oponente");
                    mostrar_tablero(computadora_tablero);
                    Console.WriteLine("\n\n\t" + "¡Has ganado!" + "\n" + "\n" + "\n");
                    break;

                }

                ataque_computadora(usuario_tablero);

                mostrar_ambos_tableros();

                if (fin_partida(usuario_tablero)) {

                    Console.WriteLine("\n\n\n\t" + "        Tu tablero");
                    mostrar_tablero(usuario_tablero);
                    Console.WriteLine("\n\t" + "¡Has perdido!" + "\n" + "\n" + "\n");
                    break;

                }

            }

            Console.ReadKey();

        }

        private void ataque(char[,] tablero, int fila, int columna) {

            char casilla_barco = 'S';

            if (tablero[fila, columna] == casilla_vacia) {

                Console.Write("\n\t" + "¡Agua!");
                tablero[fila, columna] = casilla_agua;

            } else if (tablero[fila, columna] == casilla_barco) {

                Console.Write("\n\t" + "¡Tocado!");
                tablero[fila, columna] = casilla_tocado;

            } else {

                Console.WriteLine("\n\t" + "Ya has lanzado un ataque a estas coordenadas. Prueba otra.");

            }

        }


        private void ataque_computadora(char[,] tablero) {

            Random aleatorio = new Random();

            int fila;
            int columna;

            do {

                fila = aleatorio.Next(tamaño_tablero);
                columna = aleatorio.Next(tamaño_tablero);

            } while (tablero[fila, columna] == casilla_agua || tablero[fila, columna] == casilla_tocado);

            if (tablero[fila, columna] == casilla_vacia) {

                Console.Write("\n\t" + "Oponente, ¡Agua!" + "\n");
                tablero[fila, columna] = casilla_agua;

            } else {

                Console.Write("\n\t" + "Oponente, ¡Tocado!" + "\n");
                tablero[fila, columna] = casilla_tocado;

            }

        }

        private bool fin_partida(char[,] tablero) {

            char casilla_barco = 'S';

            for (int i = 0; i < tamaño_tablero; i++) {

                for (int j = 0; j < tamaño_tablero; j++) {

                    if (tablero[i, j] == casilla_barco) {

                        return false;

                    }

                }

            }

            return true;

        }

    }

}
