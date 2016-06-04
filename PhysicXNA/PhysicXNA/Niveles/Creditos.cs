using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles
{
    /// <summary>Nivel utilizado solo para pruebas de programación.</summary>
    public class Creditos : Nivel
    {
        Color fondo;
        Texto3D texto;

        /// <summary>
        /// Construye una instancia para el nivel de pruebas.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="Nivel.cuadrosPorSegundo"/></param>
        public Creditos(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego, rutaMusica, rutaVideo, cuadrosPorSegundo)
        {
            reglas[0] = "Estos son los créditos, no deberías ni estar viendo esto.";
            reglas[1] = "Ninguno, se supone que se quitaba solo xD";
            reglas[2] = "¿Qué te pueden enseñar los créditos?\nSolo cosas que tienen que ver con el juego en sí :v";

            texto = new Texto3D("Menu/FuenteEntrada", "", Vector2.Zero, 20, Color.White);
        }

        /// <summary><seealso cref="Nivel.LoadContent"/></summary>
        protected override void LoadContent()
        {
            texto.CargarContenido(Content);
            base.LoadContent();
        }

        /// <summary><seealso cref="Nivel.UnloadContent"/></summary>
        protected override void UnloadContent()
        {
            texto.LiberarContenido();
            base.UnloadContent();
        }

        /// <summary><seealso cref="Nivel.Inicializar"/></summary>
        public override void Inicializar()
        {
            texto.Posicion = new Vector2((resolucionPantalla.Width - texto.TamTexto.X) / 2,
                (resolucionPantalla.Height - texto.TamTexto.Y) / 2);

            base.Inicializar();
        }

        /// <summary><seealso cref="Nivel.Update"/></summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (reproduciendoVideo) return;

            texto.Actualizar(gameTime);
            fondo = new Color(0,0,0);

            //if (estadoTecladoAnt.IsKeyDown(Keys.Escape) && !estadoTeclado.IsKeyDown(Keys.Escape)) nivelCompletado = true;
            nivelCompletado = true;

            estadoRatonAnt = estadoRaton;
            estadoTecladoAnt = estadoTeclado;
        }

        /// <summary><seealso cref="Nivel.Draw"/></summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (reproduciendoVideo) return;
            GraphicsDevice.Clear(fondo);
            texto.Dibujar(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
