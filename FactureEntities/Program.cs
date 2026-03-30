using FactureEntities.Entities;

namespace FactureEntities
{
    internal class Program
    {
        public static void Select()
        {
            //select * from article
            //connecter à la db
            SqlServerContext context = new SqlServerContext();
            //récupérer la liste de tous les articles
            List<Article> listArticles = context.Articles.ToList();
            //afficher tous les articles
            foreach (Article article in listArticles)
            {
                //Console.WriteLine(article.Nom);
            }

            //select top 3 numero from facture
            List<Facture> listeFactures = context.Factures.ToList();
            for (int i = 0; i <= 2; i++)
            {
                //Console.WriteLine(listeFactures[i].Numero);
            }
            //select * from article a where a.id < 1440
            listArticles = context.Articles.Where(a => a.Id < 1440).ToList();
            foreach (Article article in listArticles)
            {
                Console.WriteLine(article.Id);
            }

            //select Id from article a where a.id < 1440
            List<int> Ids = context.Articles.Where(a => a.Id < 1440).Select(a => a.Id).ToList();
            foreach (int id in Ids)
            {
                Console.WriteLine(id);
            }
        }

        public static void Insert()
        {
            try
            {
                //insert into article (...) values (...)
                SqlServerContext context = new SqlServerContext();
                Article article = new Article();
                article.Prix = 123;
                article.Description = "un nouvel article";
                article.Nom = "newArticle";
                //ajout de l'article sans l'insérer
                context.Articles.Add(article);
                //insertion de l'article
                context.SaveChanges();
                Console.WriteLine(article.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("zut : " + ex);
            }
            
        }

        public static void ControlInsert()
        {
            SqlServerContext context = new SqlServerContext();
            Article article = context.Articles.OrderBy(a => a.Id).LastOrDefault();
            Console.WriteLine($"id : {article.Id}, nom : {article.Nom}");
        }

        public static void Delete()
        {
            try
            {
                //delete from article where id = 2104
                SqlServerContext context = new SqlServerContext();
                Article article = context.Articles.Where(a => a.Id == 2104).FirstOrDefault();
                context.Articles.Remove(article);
                context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public static void Update(int id, string nom)
        {
            try
            {
                //update article set nom = 'updateArticle' where id = {id}}
                SqlServerContext context = new SqlServerContext();
                Article article = context.Articles.Where(a => a.Id == id).FirstOrDefault();
                article.Nom = nom;
                context.Articles.Update(article);
                context.SaveChanges();
                Console.WriteLine("update OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        static void Main(string[] args)
        {
            //Select();
            //Insert();
            Update(2103, "updateArticle");
            ControlInsert();
            //Delete();
        }
    }
}
