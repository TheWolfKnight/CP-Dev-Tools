using System;
using System.Collections.Generic;


namespace CP_Dev_Tools.Src
{

    public enum SpriteCollection
    {
        Idle,
        Move,
        Attack,
        Death,
    }

    public class SpriteSet
    {
        // idx 0: Idle, idx 1: Move, idx 2: Attack, idx 3: Death
        public List<Sprite>[] spriteSets = new List<Sprite>[4] {
            new List<Sprite>(),
            new List<Sprite>(),
            new List<Sprite>(),
            new List<Sprite>()
        };

        public int[] Fps { get; set; }

        public SpriteCollection CurrentSpriteSet { get; set; }

        /// <summary>
        /// Constructor function for the SpriteSet class
        /// </summary>
        /// <param name="startingSpriteSet"> the starting sprite frame that the user is working on </param>
        public SpriteSet( SpriteCollection startingSpriteSet=SpriteCollection.Idle )
        {
            Fps = new int[4] { 5, 5, 5, 5 };

            CurrentSpriteSet = startingSpriteSet;
        }

        /// <summary>
        /// Adds a sprite to the acrive sprite set
        /// </summary>
        /// <param name="item"> the sprite to be added </param>
        public void AddSpriteToSet( Sprite item )
        {
            spriteSets[(int)CurrentSpriteSet].Add(item);
        }

        public List<Sprite> GetActiveSpriteSet()
        {
            return spriteSets[(int)CurrentSpriteSet];
        }

        /// <summary>
        /// Removes a sprite from the active sprite set
        /// </summary>
        /// <param name="id"> id of the sprite that is removed </param>
        public void RemoveSpriteFromSet( int id )
        {
            removeFromArray(spriteSets[(int)CurrentSpriteSet], id);
        }


        /// <summary>
        /// Converts the spriteset to the final format of tres for the game
        /// </summary>
        /// <returns> a string containing the data for the game, to be sent strait to the WriteFile function </returns>
        public string ConvertToTRES()
        {

            const string header = "[gd_resource type=\"SpriteFrames\" load_steps=2 format=2]";
            const string extTemplate = "[ext_resource path=\"{0}\" type=\"Texture\" id={1}]";
            const string frameSetTemplate = "{{ \"frames\": [{0}]\n\"loop\": true\n\"name\": \"{1}\"\n\"speed\": {2} }}";
            string bodyTemplate = "";

            // TODO: Make the rest of the function
            return "";

        }


        public void SetActiveSpriteSet( SpriteCollection target )
        {
            CurrentSpriteSet = target;
        }


        /// <summary>
        /// Loops over the sprites in the List, then removes the sprite with a given id
        /// returns true if something was removed
        /// </summary>
        /// <param name="sprites"> the sprite list that is being looped over </param>
        /// <param name="id"> the id of the sprite to be removed </param>
        /// <returns></returns>
        private bool removeFromArray( List<Sprite> sprites, int id )
        {
            for ( int i = 0; i < sprites.Count; i++ )
            {
                if ( sprites[i].Id.Equals(id))
                {
                    sprites.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

    }

    public struct Sprite
    {
        public string Path { get; private set; }
        public int Id { get; private set; }

        public Sprite( string path, int id )
        {
            Path = path;
            Id = id;
        }

    }


}
