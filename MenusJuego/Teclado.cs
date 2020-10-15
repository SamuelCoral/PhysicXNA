using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicXNA.Nucleo;

namespace PhysicXNA.MenusJuego
{
    /// <summary>Clase que contiene métodos auxiliares para dibujar una barra de texto.</summary>
    public class Teclado
    {
        private Stack<char> caracteres;
        internal DuendeEstatico barra;
        private DuendeEstatico cursor;
        private Texto3D textoDibujar;
        /// <summary>Indica la velocidad de parpadeo del cursor.</summary>
        public int cuadrosPorSegundo;
        private int tiempoJuego;
        private bool mostrarCursor;
        private Keys[] teclasPresionadas;

        /// <summary>Texto de la barra.</summary>
        public String Texto
        {
            get
            {
                return new String(caracteres.Reverse().ToArray());
            }

            set
            {
                caracteres = new Stack<char>(value.ToArray());
                textoDibujar.Texto = value;
            }
        }

        /// <summary>
        /// Construye una instancia de esta clase.
        /// </summary>
        /// <param name="cuadrosPorSegundo"><seealso cref="cuadrosPorSegundo"/></param>
        /// <param name="textoDefault">Texto por default.</param>
        public Teclado(int cuadrosPorSegundo, String textoDefault = "")
        {
            caracteres = new Stack<char>(textoDefault.ToArray());
            textoDibujar = new Texto3D("Menu/FuenteEntrada", textoDefault, Vector2.Zero, 30, Color.DarkGreen);
            barra = new DuendeEstatico("Menu/BarraTexto", Vector2.Zero);
            cursor = new DuendeEstatico("Menu/CursorTexto", Vector2.Zero, Color.White, new Vector2(0.3f, 0.3f));
            this.cuadrosPorSegundo = cuadrosPorSegundo;
            tiempoJuego = 0;
            mostrarCursor = true;
        }

        /// <summary>
        /// Carga el contenido para cada elemento del teclado.
        /// </summary>
        /// <param name="contenido">Administrador de contenidos del juego para cargar el contenido.</param>
        public void CargarContenido(ContentManager contenido)
        {
            barra.CargarContenido(contenido);
            textoDibujar.CargarContenido(contenido);
            cursor.CargarContenido(contenido);
        }

        /// <summary>Libera los recursos ocupados por los elementos del botón del menú.</summary>
        public void LiberarContenido()
        {
            barra.LiberarContenido();
            textoDibujar.LiberarContenido();
            cursor.LiberarContenido();
        }

        /// <summary>
        /// Actualiza el estado del teclado.
        /// </summary>
        /// <param name="menuOrigen">Juego de origen.</param>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public void Actualizar(Menu menuOrigen, GameTime tiempoJuego)
        {
            this.tiempoJuego += tiempoJuego.ElapsedGameTime.Milliseconds;
            if (this.tiempoJuego >= 1000 / cuadrosPorSegundo)
            {
                this.tiempoJuego %= 1000 / cuadrosPorSegundo;
                mostrarCursor = !mostrarCursor;
            }

            teclasPresionadas = menuOrigen.estadoTeclado.GetPressedKeys();
            if (teclasPresionadas.Length - (menuOrigen.estadoTeclado.IsKeyDown(Keys.LeftShift) ? 1 : 0) - (menuOrigen.estadoTeclado.IsKeyDown(Keys.RightShift) ? 1 : 0) == 1
                && menuOrigen.estadoTecladoAnt.GetPressedKeys().Length - (menuOrigen.estadoTeclado.IsKeyDown(Keys.LeftShift) ? 1 : 0) - (menuOrigen.estadoTeclado.IsKeyDown(Keys.RightShift) ? 1 : 0) == 0)
            {
                if (teclasPresionadas[0] == Keys.Back && caracteres.Count > 0) caracteres.Pop();
                if (caracteres.Count < SistemaPerfiles.ListaPerfilesJugador.tamNombresJugador - 1)
                {
                    if (teclasPresionadas[0] == Keys.Space) caracteres.Push(' ');
                    if (teclasPresionadas[0] >= Keys.A && teclasPresionadas[0] <= Keys.Z)
                    {
                        String l = teclasPresionadas[0].ToString();
                        if (!menuOrigen.estadoTeclado.IsKeyDown(Keys.LeftShift) && !menuOrigen.estadoTeclado.IsKeyDown(Keys.RightShift)) l = l.ToLower();
                        caracteres.Push(l[0]);
                    }
                    if (teclasPresionadas[0] >= Keys.D0 && teclasPresionadas[0] <= Keys.D9) caracteres.Push(teclasPresionadas[0].ToString()[1]);
                    if (teclasPresionadas[0] >= Keys.NumPad0 && teclasPresionadas[0] <= Keys.NumPad9) caracteres.Push(teclasPresionadas[0].ToString()[6]);
                }
            }

            barra.Escala = new Vector2((float)(menuOrigen.dimensiones.Width - 40) / barra.AnchoReal, 1f);
            barra.Posicion = new Vector2(20f, (float)(menuOrigen.dimensiones.Height - barra.Alto) / 2);
            textoDibujar.Texto = Texto;
            textoDibujar.Posicion = new Vector2(barra.Posicion.X + (barra.Ancho - textoDibujar.TamTexto.X) / 2, barra.Posicion.Y + (barra.Alto - textoDibujar.TamTexto.Y) / 2 + 8);
            textoDibujar.Actualizar(tiempoJuego);
            textoDibujar.Escala = new Vector2(1f);
            cursor.Posicion = new Vector2(textoDibujar.Posicion.X + textoDibujar.TamTexto.X, barra.Posicion.Y + (barra.Alto - cursor.Alto) / 2);
        }

        /// <summary>
        /// Dibuja los elementos del teclado.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="tiempoJuego"></param>
        public void Dibujar(SpriteBatch spriteBatch, GameTime tiempoJuego)
        {
            barra.Dibujar(spriteBatch, tiempoJuego);
            textoDibujar.Dibujar(spriteBatch, tiempoJuego);
            if(mostrarCursor) cursor.Dibujar(spriteBatch, tiempoJuego);
        }
    }
}
