using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawHexagon;

#region Autodesk libraries
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
#endregion

[assembly: CommandClass(typeof(DrawHexagon.Commands))]

namespace DrawHexagon
{
    class Commands
    {
        [CommandMethod("xHexa")]
        public void Start()
        {
            Database db = Active.Database;
            Hexagon coord = new Hexagon();
            List<Midpoint> list = coord.DrawHexGrid(0, 100, 0, 100, 5);
            Point3dCollection p3 = new Point3dCollection();

            try
            {
                foreach (Midpoint pt in list)
                {
                    p3.Add(new Point3d(pt.X, pt.Y, 0));
                }




                using (Transaction acTrans = db.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    if (p3 != null)
                    {
                        foreach (Point3d pt in p3)
                        {
                            using (DBPoint acPoint = new DBPoint(pt))
                            {
                                // Add the new object to the block table record and the transaction
                                acBlkTblRec.AppendEntity(acPoint);
                                acTrans.AddNewlyCreatedDBObject(acPoint, true);
                            }
                        }
                    }
                    // Set the style for all point objects in the drawing
                    db.Pdmode = 34;
                    db.Pdsize = 0.1;

                    // Save the new object to the database
                    acTrans.Commit();
                }
            }
            catch(System.Exception ex) { }

            
        }

    }

    public static class Active
    {
        /// <summary>
        /// Returns the active Editor object.
        /// </summary>
        public static Editor Editor
        {
            get { return Document.Editor; }
        }

        /// <summary>
        /// Returns the active Document object.
        /// </summary>
        public static Document Document
        {
            get { return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument; }
        }

        /// <summary>
        /// Returns the active Database object.
        /// </summary>
        public static Database Database
        {
            get { return Document.Database; }
        }

        /// <summary>
        /// Sends a string to the command line in the active Editor
        /// </summary>
        /// <param name="message">The message to send.</param>
        public static void WriteMessage(string message)
        {
            Editor.WriteMessage(message);
        }

        /// <summary>
        /// Sends a string to the command line in the active Editor using String.Format.
        /// </summary>
        /// <param name="message">The message containing format specifications.</param>
        /// <param name="parameter">The variables to substitute into the format string.</param>
        public static void WriteMessage(string message, params object[] parameter)
        {
            Editor.WriteMessage(message, parameter);
        }
    }

    public class Extension : IExtensionApplication
    {
        [CommandMethod("info")]
        public void Initialize()
        {
            Active.Editor.WriteMessage("\n-> Get Points from Hexagon: xHexa");
        }

        public void Terminate()
        {
        }
    }

}
