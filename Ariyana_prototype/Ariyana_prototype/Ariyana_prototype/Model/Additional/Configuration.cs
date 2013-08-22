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

namespace Ariyana_prototype
{
    class Configuration : Game1
    {
       private static volatile Configuration instance;
       private static object syncRoot = new Object();
       public static Vector2 Resolution = new Vector2();

       private Configuration() { }

       // Singleton
       public static Configuration Instance
       {
          get 
          {
             if (instance == null) 
             {
                lock (syncRoot) 
                {
                   if (instance == null)
                       instance = new Configuration();
                }
             }
             return instance;
          }
       }

       // Used for switching between developing on laptop/pc
       public void Initialize(GraphicsDeviceManager graphics)
       {
           graphics.PreferredBackBufferWidth = 1920;
           graphics.PreferredBackBufferHeight = 1080;

           Resolution.X = 1920;
           Resolution.Y = 1080;

           bool workingOnLaptop = false;
           if(workingOnLaptop)
           {
               graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
           }

           graphics.IsFullScreen = true;
           graphics.ApplyChanges();

           gameState = GameStates.TitleScreen;
           titleState = TitleStates.Start;
       }

       void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
       {
           DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
           e.GraphicsDeviceInformation.PresentationParameters.BackBufferFormat = displayMode.Format;
           e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = displayMode.Width;
           e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = displayMode.Height;

           Resolution.X = displayMode.Width;
           Resolution.Y = displayMode.Height;
       }

       public Vector2 getResolution()
       {
           return Resolution;
       }

    }
}
