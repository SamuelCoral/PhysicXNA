using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel5
{
    /// <summary>
    /// Representa un objeto en el espacio que tiene influencia por la fuerza de gravedad de otros objetos.
    /// </summary>
    public interface iGravitatorio
    {
        /// <summary>Masa del objeto en Kilogramos.</summary>
        double Masa { get; set; }
        /// <summary>Fuerza a aplicar en Newtons.</summary>
        double Fuerza { get; set; }
        /// <summary>Ángulo de la fuerza aplicada en Radianes.</summary>
        double AnguloFuerza { get; set; }
        /// <summary>Aceleración del objeto en Metros x Segundo cuadrado para un instante.</summary>
        Vector2 Aceleracion { get; set; }
        /// <summary>Velocidad del objeto en Metros x Segundo.</summary>
        Vector2 Velocidad { get; set; }
        /// <summary>Diferencial de posición a aplicar en Metros después de haber calculado la sumatoria de velocidades.</summary>
        Vector2 IncrementoPosicion { get; set; }
        /// <summary>Posición absoluta en el escenario del objeto.</summary>
        Vector2 PosicionAbsoluta { get; set; }
        /// <summary>Tamaño bidimensional del objeto en pixeles.</summary>
        Vector2 Tamaño { get; }

        /// <summary>
        /// Obtiene el incremento de velocidad para el objeto actual
        /// que se da en un tiempo determinado al estar a una cierta distancia
        /// de otro objeto en el espacio.
        /// </summary>
        /// <param name="objeto">Objeto en el espacio a ser influido.</param>
        /// <param name="tiempoTranscurrido">Tiempo que ha transcurrido para tomar en cuenta en los cálculos.</param>
        void ObtenerIncrementoPosicion(iGravitatorio objeto, TimeSpan tiempoTranscurrido);
    }
}
