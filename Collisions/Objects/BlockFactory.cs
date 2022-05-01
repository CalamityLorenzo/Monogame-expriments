using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Threading.Tasks.Dataflow;

namespace Collisions.Objects
{

    public enum BlockType
    {
        None = 0,
        Basic = 1,
        Bump=2
    }
    public class BlockFactory
    {
        private Dictionary<string, Texture2D> createdBlocks = new Dictionary<string, Texture2D>();
        private SpriteBatch spriteBatch;
        private readonly Dimensions dimensions;
        private List<Texture2D> blockTemplates;

        public BlockFactory(SpriteBatch spriteBatch, IList<Texture2D>templates, Dimensions dimensions)
        {
            this.spriteBatch = spriteBatch;
            this.dimensions = dimensions;
            this.blockTemplates = new List<Texture2D>(templates);
        }

        public Texture2D GetBlock(BlockType block, Color colour)
        {
            var key = $"{block.ToString()}_{colour}";
            if(!createdBlocks.ContainsKey(key))
            {
                var blockTypeTexture = blockTemplates[(int)block - 1];
                var typeData = new Color[dimensions.Width * dimensions.Height];
                blockTypeTexture.GetData(typeData);

                // iterate throught the block data (t2)
                // Where the Alpha is >0 ( so being used)
                // replace it with the colour of choice

                for(var x =0;x<typeData.Length;++x)
                {
                    var itm = typeData[x];
                    if (itm.A != 255)
                    {
                        itm.R += colour.R;
                        itm.G += colour.G;
                        itm.B += colour.B;
                        if (itm.A == 252) 
                            ;
                        if (itm.A == 0) itm.A = 255;
                        //itm.A = colour.A;
                    }
                    typeData[x] = itm;
                }

                var completeTexture = new Texture2D(this.spriteBatch.GraphicsDevice, dimensions.Width, dimensions.Height);
                completeTexture.SetData<Color>(typeData);
                createdBlocks.Add(key, completeTexture);
            }

            return createdBlocks[key];
        }
    }
}
