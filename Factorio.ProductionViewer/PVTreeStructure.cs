using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    public class PVTreeStructure : IPVLogic
    {

        #region Private Variables



        private PVSettings m_settings;
        private FactorioAssembly m_parentAssembly;
        private List<IPVLine> m_lines;
        private List<IPVImage> m_images;

        private int m_currentPossition;



        #endregion

        #region Interface Properties



        /// <summary>
        /// images which are shown
        /// </summary>
        public List<IPVImage> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<IPVImage>();
                return m_images;
            }
            private set { m_images = value; }
        }

        /// <summary>
        /// lines which connects the pictures
        /// </summary>
        public List<IPVLine> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<IPVLine>();
                return m_lines;
            }
            private set { m_lines = value; }
        }



        #endregion

        #region Properties



        /// <summary>
        /// Settings for this prduction viewer
        /// </summary>
        public PVSettings Settings
        {
            get
            {
                if (m_settings == null)
                    m_settings = new PVSettings();
                return m_settings;
            }
        }

        /// <summary>
        /// Public accessor for the parent assembly of the tree structure
        /// </summary>
        public FactorioAssembly ParentAssembly
        {
            get { return m_parentAssembly; }
            private set { m_parentAssembly = value; }
        }



        #endregion

        #region Constructors



        /// <summary>
        /// create a new tree struction for the prorduction viewer and use the data from the parameter
        /// </summary>
        /// <param name="assembly">build a tree structure out of this data</param>
        public PVTreeStructure(FactorioAssembly assembly)
        {
            this.ParentAssembly = assembly;
            buildTreeStructure(this.ParentAssembly);
        }



        #endregion

        #region Public Methods



        /// <summary>
        /// rebuild this tree structure
        /// </summary>
        public void Rebuild()
        {
            this.Lines = new List<IPVLine>();
            this.Images = new List<IPVImage>();
            buildTreeStructure(this.ParentAssembly);
        }



        #endregion

        #region Private Methods



        /// <summary>
        /// build the tree structure out of the given assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="position"></param>
        /// <param name="level"></param>
        private PVImage buildTreeStructure(FactorioAssembly assembly, int position = 0, int level = 0)
        {
            // create image and set values
            var image = new PVImage(assembly, this.Settings);
            image.PositionStart = position;
            image.Level = level;
            

            // save the latest position value
            m_currentPossition = position;


            // add a reference to the image list
            this.Images.Add(image);


            // check if this assembly has sub assemblys
            if (assembly.SubAssembly.Count == 0)
                // if not set the position end to the same value as the start value
                image.PositionEnd = position; 
            else
            {
                // loop through all sub assemblys
                for (int i = 0; i < assembly.SubAssembly.Count; i++)
                {
                    var addValue = i <= 1 ? i : 1;      // the increes value can only be 0 or 1. It is related to the counter 'i', so for the first etheration it is 0 and after that it is always 1.
                    var curLevel = level + 1;           // get the next level
                    var curPos = addValue + position;   // get the next position 


                    // create the next image
                    var subImage = buildTreeStructure(assembly.SubAssembly[i], curPos, curLevel);


                    // create a line between the image and ist sub image
                    this.Lines.Add(new PVLine(image, subImage));


                    // override the old position with the new furthest position
                    position = m_currentPossition;
                }

                // set the position end value
                image.PositionEnd = position;
            }
            
            return image;
        }

        


        #endregion
        
    }
}
