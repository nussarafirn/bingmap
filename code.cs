using System;
using System.Collections.Generic;

namespace coding_challenge
{
    class Coordinates
    {
        // represents latitude
        public double lat;

        // represents longtitude
        public double lng;

        // Constructs the Coordinte class with the given latitude and longtitude
        public Coordinates(double theLat, double theLng)
        {
            lat = theLat;
            lng = theLng;
        }

        // Generates random n coorinates with the given origin coordinate and radius
        public static List<Coordinates> GenerateRandCor(Coordinates cor, double radius, int n)
        {
            Random rand = new Random();
            List<Double> convertListX = new List<Double>();
            List<Double> convertListY = new List<Double>();
            List<Coordinates> results = new List<Coordinates>();
            double latToKm = 111.32;
            double circleAng = 360;

            double y0 = cor.lat * latToKm;
            double x0 = (cor.lng * 360) / Math.Cos(cor.lat);

            while (results.Count < n)
            {
                double randRad = rand.NextDouble() * radius;
                double randAngle = rand.NextDouble() * circleAng;

                double x = randRad * Math.Cos(randAngle);
                double y = randRad * Math.Sin(randAngle);
                bool isToAdd = true;

                for (int j = 0; j < convertListX.Count; j++)
                {
                    double dist2Points = Math.Sqrt(Math.Pow(convertListX[j] - x, 2) +
                                                    Math.Pow(convertListY[j] - y, 2));

                    if (dist2Points < 0.4)
                    {
                        isToAdd = false;
                    }
                }

                if (isToAdd)
                {
                    double newLat = (y + y0) / latToKm;
                    double newLong = ((x + x0) * Math.Cos(newLat)) / 360;
                    results.Add(new Coordinates(newLat, newLong));
                    convertListX.Add(x);
                    convertListY.Add(y);
                }

            }

            return results;
        }

        // Main method to run and print out results
        public static void Main(String[] args)
        {
            Coordinates cor = new Coordinates(47.260705, -122.482980);
            List<Coordinates> cors = GenerateRandCor(cor, 4, 20);
            Console.WriteLine("The results coordinates in [latitude, longtitude]:");
            Console.WriteLine();
            for (int i = 0; i < cors.Count; i++)
            {
                Console.WriteLine("    " + "[" + cors[i].lat + ", " + cors[i].lng + "]");
            }
        }
    }


    class Route
    {
        // represents the disteance
        public double distance;

        // represents the time travelling
        public double time;

        // represents the travelled route 
        public List<Coordinates> maneuverPoints;

        public Route(double theDistance, double theTime, List<Coordinates> theManeuverPoints)
        {
            distance = theDistance;
            time = theTime;
            maneuverPoints = theManeuverPoints;
        }

        // generates API url with the given to-coordinate and from-coordinate
        public static String GenerateRequestUrl(Coordinates toCoor, Coordinates fromCoor)
        {
            var key = "AvIOkpXMA-gopXAfntDX_GCe4t9K1ONIig5SZ762rjCrTZWk2tDA7QzAY3u7ojsP";
            var baseURL = "https://bing.com/maps/default.aspx";
            var filler = "?";
            var url = baseURL + filler + "origins=" + toCoor.lat + "," + toCoor.lng + "&" +
                 "destinations=" + fromCoor.lat + "," + fromCoor.lng + "&" + "key=" + key;
            return url;
        }
    }
}
