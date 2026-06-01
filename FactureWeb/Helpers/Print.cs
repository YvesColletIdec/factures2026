using FactureEntities.Entities;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;

namespace FactureWeb.Helpers
{
    public class Print
    {
        //sdfasdf
        /*
         * fdg
         * df
         * dfg
         * df
         * df*/
        /**
         * @summary elle fait un pdf
         * returns retourne un chemin
         * <exception cref="peut lever une eecption"
         */
        public static string CreateDocumentFromTemplateWithFormat(Facture f, string template)
        {
            Document document = new Document();
            document.LoadFromFile(template);
            document.Replace("client_prenom", f.Client.Prenom, true, true);
            document.Replace("client_nom", f.Client.Nom, true, true);
            document.Replace("client_adresse", f.Client.Adresse, true, true);
            document.Replace("client_npa", f.Client.Npa.ToString(), true, true);
            document.Replace("client_localite", f.Client.Localite, true, true);
            document.Replace("facture_date", f.DateFacture.ToString("dd.MM.yyyy"), true, true);
            document.Replace("facture_num", f.Numero, true, true);

            //la première section est celle ou l'on trouve un titre "Titre1"
            Section s = document.Sections[0];
            Table table = s.AddTable(true);
            String[] Header = { "N°", "Article", "Quantité", "Prix unitaire", "Total" };
            table.ResetCells(f.LigneFactures.Count + 1, Header.Length);

            //Header Row
            TableRow FRow = table.Rows[0];
            FRow.IsHeader = true;
            //Row Height
            //FRow.Height = 18;
            FRow.Cells[0].SetCellWidth(20, CellWidthType.Point);
            FRow.Cells[1].SetCellWidth(150, CellWidthType.Point);
            FRow.Cells[2].SetCellWidth(60, CellWidthType.Point);
            FRow.Cells[3].SetCellWidth(80, CellWidthType.Point);
            FRow.Cells[4].SetCellWidth(40, CellWidthType.Point);
            //Header Format
            FRow.RowFormat.BackColor = Color.LightBlue;
            for (int i = 0; i < Header.Length; i++)
            {
                //Cell Alignment
                Paragraph p = FRow.Cells[i].AddParagraph();
                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                //Data Format
                TextRange TR = p.AppendText(Header[i]);
                TR.CharacterFormat.FontName = "Calibri";
                TR.CharacterFormat.FontSize = 14;
                TR.CharacterFormat.TextColor = Color.Teal;
                TR.CharacterFormat.Bold = true;
            }

            decimal grandTotal = 0;
            List<LigneFacture> listLignesFactures = new List<LigneFacture>(f.LigneFactures);
            //Data Row
            for (int r = 0; r < listLignesFactures.Count; r++)
            {
                LigneFacture lf = listLignesFactures[r];
                TableRow DataRow = table.Rows[r + 1];

                //Row Height
                DataRow.Height = 15;

                //C Represents Column. 5 -> nombre de colonnes
                for (int c = 0; c < 5; c++)
                {
                    //Cell Alignment
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    //Fill Data in Rows
                    Paragraph p2 = DataRow.Cells[c].AddParagraph();
                    TextRange TR2 = null;
                    decimal total = lf.PrixUnitaire * lf.Quantite;
                    switch (c)
                    {
                        case 0:
                            TR2 = p2.AppendText(lf.Article.Id.ToString());
                            DataRow.Cells[c].SetCellWidth(20f, CellWidthType.Point);
                            break;
                        case 1:
                            TR2 = p2.AppendText(lf.Article.Nom);
                            DataRow.Cells[c].SetCellWidth(150f, CellWidthType.Point);
                            break;
                        case 2:
                            TR2 = p2.AppendText(lf.Quantite.ToString());
                            DataRow.Cells[c].SetCellWidth(60f, CellWidthType.Point);
                            break;
                        case 3:
                            TR2 = p2.AppendText(lf.PrixUnitaire.ToString());
                            DataRow.Cells[c].SetCellWidth(80f, CellWidthType.Point);
                            break;
                        case 4:
                            TR2 = p2.AppendText(total.ToString());
                            DataRow.Cells[c].SetCellWidth(40f, CellWidthType.Point);
                            grandTotal += total;
                            break;
                        default:
                            Console.WriteLine("Erreur dans le numéro de colonne");
                            break;
                    }

                    //Format Cells
                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    TR2.CharacterFormat.FontName = "Calibri";
                    TR2.CharacterFormat.FontSize = 12;
                    TR2.CharacterFormat.TextColor = Color.Brown;
                }

            }
            //TOTAL
            Paragraph pa = s.AddParagraph();
            pa.AppendText("\n");
            TextRange t = pa.AppendText($"TOTAL : {f.TotalFacture}");
            pa.Format.HorizontalAlignment = HorizontalAlignment.Right;
            t.CharacterFormat.FontName = "Calibri";
            t.CharacterFormat.FontSize = 16;
            t.CharacterFormat.TextColor = Color.SteelBlue;
            string save = @"c:\adi\xxx.pdf";
            document.SaveToFile(save, FileFormat.PDF);
            return save;
        }

    }
}
