using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Algo
{
    public class RecoContext
    {
        public User[] Users { get; private set; }
        public Movie[] Movies { get; private set; }

        public void LoadFrom( string folder )
        {
            Users = User.ReadUsers( Path.Combine( folder, "users.dat" ) );
            Movies = Movie.ReadMovies( Path.Combine( folder, "movies.dat" ) );
            User.ReadRatings( Users, Movies, Path.Combine( folder, "ratings.dat" ) );
        }

        public double SimilarityBetween( User u1, User u2, Func<User, User, double> distance = null )
        {
            if( distance == null ) distance = DistanceNom2;
            double d = distance( u1, u2 );
            return 1 / (d + 1);
        }

        public double SimilarityPearson( User u1, User u2 )
        {
            IEnumerable<Movie> common = u1.Ratings.Keys.Intersect( u2.Ratings.Keys );
            int count = common.Count();
            if( count == 0 ) return 0;
            if( count == 1 ) return SimilarityBetween(u1, u2);

            double sumProd = 0;
            double sumSquare1 = 0;
            double sumSquare2 = 0;
            double sum1 = 0;
            double sum2 = 0;

            foreach( Movie m in common )
            {
                int r1 = u1.Ratings[m];
                int r2 = u2.Ratings[m];
                sum1 += r1;
                sum2 += r2;
                sumSquare1 += r1 * r1;
                sumSquare2 += r2 * r2;
                sumProd += r1 * r2;
            }

            double numerator = sumProd - ((sum1 * sum2) / count);
            double denominator1 = sumSquare1 - ((sum1 * sum1) / count);
            double denominator2 = sumSquare2 - ((sum2 * sum2) / count);
            double denominator = Math.Sqrt( denominator1 * denominator2 );
            return numerator / denominator;
        }

        private double DistanceNom2(User u1, User u2)
        {
            var common = u1.Ratings.Keys.Intersect(u2.Ratings.Keys);
            if (!common.Any()) return -1.0;//si aucun films en commun
            double sum = 0.0;
            foreach (Movie m in common)
            {
                var r1 = u1.Ratings[m];
                var r2 = u2.Ratings[m];
                double delta = Math.Abs(r1 - r2);
                sum += delta * delta;
            }
            return Math.Sqrt(sum);
        }

        #region Version de DistanceNorm1 petit joueur
        private double DistanceNom1(User u1, User u2)
        {
            var common = u1.Ratings.Keys.Intersect(u2.Ratings.Keys);
            if (!common.Any()) return -1.0;
            double sum = 0.0;
            foreach (Movie m in common)
            {
                var r1 = u1.Ratings[m];
                var r2 = u2.Ratings[m];
                sum += Math.Abs(r1 - r2);
            }
            return sum;
        }
        #endregion

        #region Version de DistanceNorm1 plus évoluée
        private double DistanceNom1Evoluee(User u1, User u2)
        {
            var common = u1.Ratings.Keys.Intersect(u2.Ratings.Keys);
            if (!common.Any()) return -1.0;
            var x = common.Select(m => Math.Abs(u1.Ratings[m] - u2.Ratings[m])).Sum();// c'est un intersect
            return x;
        }
        #endregion

        private double DistanceNomInfinie(User u1, User u2)
        {
            var common = u1.Ratings.Keys.Intersect(u2.Ratings.Keys);
            if (!common.Any()) return -1.0;
            return common.Select(m => Math.Abs(u1.Ratings[m] - u2.Ratings[m])).Max();
        }

    }
}
