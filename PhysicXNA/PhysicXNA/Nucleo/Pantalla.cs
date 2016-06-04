using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PhysicXNA
{
    /// <summary>Estructura que contiene toda la información sobre la configuración de pantalla.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMODE
    {
        private const int CCHDEVICENAME = 0x20;
        private const int CCHFORMNAME = 0x20;
        /// <summary></summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        /// <summary></summary>
        public short dmSpecVersion;
        /// <summary></summary>
        public short dmDriverVersion;
        /// <summary></summary>
        public short dmSize;
        /// <summary></summary>
        public short dmDriverExtra;
        /// <summary></summary>
        public int dmFields;
        /// <summary></summary>
        public int dmPositionX;
        /// <summary></summary>
        public int dmPositionY;
        /// <summary></summary>
        public int dmDisplayOrientation;
        /// <summary></summary>
        public int dmDisplayFixedOutput;
        /// <summary></summary>
        public short dmColor;
        /// <summary></summary>
        public short dmDuplex;
        /// <summary></summary>
        public short dmYResolution;
        /// <summary></summary>
        public short dmTTOption;
        /// <summary></summary>
        public short dmCollate;
        /// <summary></summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        /// <summary></summary>
        public short dmLogPixels;
        /// <summary></summary>
        public int dmBitsPerPel;
        /// <summary></summary>
        public int dmPelsWidth;
        /// <summary></summary>
        public int dmPelsHeight;
        /// <summary></summary>
        public int dmDisplayFlags;
        /// <summary></summary>
        public int dmDisplayFrequency;
        /// <summary></summary>
        public int dmICMMethod;
        /// <summary></summary>
        public int dmICMIntent;
        /// <summary></summary>
        public int dmMediaType;
        /// <summary></summary>
        public int dmDitherType;
        /// <summary></summary>
        public int dmReserved1;
        /// <summary></summary>
        public int dmReserved2;
        /// <summary></summary>
        public int dmPanningWidth;
        /// <summary></summary>
        public int dmPanningHeight;
    }

    /// <summary>Clase que contiene métodos para obtener información sobre la pantalla.</summary>
    public static class Pantalla
    {
        /// <summary>
        /// Obtiene una configuración de pantalla.
        /// </summary>
        /// <param name="deviceName">Nombre del dispositivo o null para la pantalla primaria.</param>
        /// <param name="modeNum">Configuración a tomar.</param>
        /// <param name="devMode">Estructura de información sobre la que se guardarán los datos obtenidos.</param>
        /// <returns>false en caso de que la configuración no exista.</returns>
        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        /// <summary>Configuración de pantalla actual.</summary>
        public const int ENUM_CURRENT_SETTINGS = -1;
        /// <summary>Configuración de pantalla almacenada en el registro de Windows.</summary>
        public const int ENUM_REGISTRY_SETTINGS = -2;

        /// <summary>
        /// Obtiene el listado de las resoluciones de pantalla permitidas.
        /// </summary>
        /// <returns>Lista de cadenas de las resoluciones permitidas en el formato "Ancho"x"Alto".</returns>
        public static List<String> ObtenerResoluciones()
        {
            List<String> r = new List<string>();

            DEVMODE vDevMode = new DEVMODE(), aDevMode = new DEVMODE(), devMode = new DEVMODE();
            EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);
            
            int i = 0, c;
            bool repetido;
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                i++;
                //if (vDevMode.dmBitsPerPel != devMode.dmBitsPerPel || vDevMode.dmDisplayFrequency != devMode.dmDisplayFrequency) continue;
                repetido = false;
                for (c = i - 2; c >= 0; c--)
                {
                    EnumDisplaySettings(null, c, ref aDevMode);
                    if (aDevMode.dmPelsHeight == vDevMode.dmPelsHeight && aDevMode.dmPelsWidth == vDevMode.dmPelsWidth)
                    {
                        repetido = true;
                        break;
                    }
                }

                if (!repetido && vDevMode.dmPelsWidth >= 800 && vDevMode.dmPelsHeight >= 600)
                    r.Add(vDevMode.dmPelsWidth.ToString() + " x " + vDevMode.dmPelsHeight.ToString());
            }

            return r;
        }
    }
}
