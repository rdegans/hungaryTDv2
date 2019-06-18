using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace hungaryTDv2
{
    public class Enemy
    {
        public ImageBrush enemyFill = new ImageBrush();
        public BitmapImage bi;
        public Type type;
        public enum Type { apple, pizza, donut, hamburger, fries }
        public Canvas cEnemies = new Canvas();
        public Canvas cBackground = new Canvas();
        public Rectangle sprite = new Rectangle();
        public int speed;
        public int health;
        public int damage;
        public Point[] track;
        public int[] positions;
        public int reward;
        public int position = 0;
        public Enemy(int ty, Canvas cE, Canvas cB, Point[] tr, int[] p)
        {
            type = (Type)ty;
            cEnemies = cE;
            cBackground = cB;
            track = tr;
            positions = p;
            if (type == Type.apple)
            {
                bi = new BitmapImage(new Uri("apple.png", UriKind.Relative));
                speed = 3;
                health = 150;
                damage = 50;
                reward = 25;
            }
            else if (type == Type.pizza)
            {
                bi = new BitmapImage(new Uri("pizza.png", UriKind.Relative));
                speed = 5;
                health = 200;
                damage = 50;
                reward = 25;
            }
            else if (type == Type.donut)
            {
                bi = new BitmapImage(new Uri("donut.png", UriKind.Relative));
                speed = 3;
                health = 250;
                damage = 100;
                reward = 50;
            }
            else if (type == Type.hamburger)
            {
                bi = new BitmapImage(new Uri("hamburger.png", UriKind.Relative));
                speed = 2;
                health = 5000;
                damage = 200;
                reward = 1000;
            }
            else if (type == Type.fries)
            {
                bi = new BitmapImage(new Uri("fries.png", UriKind.Relative));
                speed = 10;
                health = 350;
                damage = 50;
                reward = 50;
            }
            enemyFill = new ImageBrush(bi);
            sprite.Fill = enemyFill;
            sprite.Height = 50;
            sprite.Width = 50;
            Canvas.SetLeft(sprite, track[position].X - 25);
            Canvas.SetTop(sprite, track[position].Y - 25);
            cEnemies.Children.Add(sprite);
            cBackground.Children.Remove(cEnemies);
            cBackground.Children.Add(cEnemies);
        }
        public int update(int index)
        {
            positions[position] = -1;//sets current position to -1 or vacant
            for (int i = 1; i < 10; i++)
            {
                if (position + i < positions.Length)
                {
                    positions[position + i] = -1;
                }
                if (position - i > -1)
                {
                    positions[position - i] = -1;//sets surrounding positions to -1 or vacant
                }
            }


            if (position < 1450 - speed - 9)//Checks if the enemy is at the end of the track
            {
                for (int i = 0; i < speed + 1; i++)//Loops up to the speed of the enemy
                {
                    if (positions[position + i + 9] != -1) //checks if the end of the range + i is vacant
                    {
                        position = position + i - 1;//finds the first vacant position, then goes to that positions - 1
                        positions[position] = index;
                        for (int x = 1; x < 10; x++)
                        {
                            if (position + x < positions.Length)
                            {
                                positions[position + x] = index;
                            }
                            if (position - x > -1)
                            {
                                positions[position - x] = index;
                            }
                        }
                        break;
                    }
                    else if (i == speed && positions[position + i] == -1)//exception where the tower gets to its speed and all those positions are empty
                    {
                        position = position + i - 1;
                        positions[position] = index;
                        for (int x = 1; x < 10; x++)
                        {
                            if (position + x < positions.Length)
                            {
                                positions[position + x] = index;
                            }
                            if (position - x > -1)
                            {
                                positions[position - x] = index;
                            }
                        }
                        break;
                    }
                }
                Canvas.SetLeft(sprite, track[position].X - 25);
                Canvas.SetTop(sprite, track[position].Y - 25);
                return 0;
            }
            else
            {
                cEnemies.Children.Remove(sprite);
                return damage;
            }
        }
    }
}
/*                        try
            {
                sw = new StreamWriter("line1.txt");
            }
            catch (FileNotFoundException)
            {
                sw = new StreamWriter("line1.txt");
            }
            for (int i = 0; i < 175; i+=1)
            {
                sw.WriteLine(i + ",275");
            }
            for (int i = 275; i > 205; i-=1)
            {
                sw.WriteLine("175," + i);
            }
            for (int i = 175; i < 315; i += 1)
            {
                sw.WriteLine(i + ",205");
            }
            for (int i = 205; i < 490; i += 1)
            {
                sw.WriteLine("315," + i);
            }
            for (int i = 315; i < 525; i += 1)
            {
                sw.WriteLine(i + ",490");
            }
            for (int i = 490; i > 275; i-=1)
            {
                sw.WriteLine("525," + i);
            }
            for (int i = 525; i < 675; i += 1)
            {
                sw.WriteLine(i + ",275");
            }
            for (int i = 275; i < 345; i++)
            {
                sw.WriteLine("675," + i);
            }
            for (int i = 675; i < 810; i += 1)
            {
                sw.WriteLine(i + ",345");
            }
            sw.Close();
*/




/*            /*
             * track:
0,240
140,240
140,170
350,170
350,455
490,455
490,240
700,240
700,310
750,310
750,210
845,210
845,380
820,410
730,380
630,380
630,310
560,310
560,525
280,525
280,240
210,240
210,310
0,310
           * menu:
0,120
845,120
845,650
1125,650
1125,0
0,0
0,120
            * polyline
0,275
175,275
175,205
315,205
315,490
525,490
525,275
675,275
675,345
810,345
        * fork
2.666666666667,19.333333333333
6.666666666667,16.666666666667
6.666666666667,6
10,3.333333333333
10,-0.6666666666666
0.666666666667,0
-0.6666666666666,4.666666666667
2.666666666667,6.666666666667
             */
/*Welcome to the first level! A wave of angry food is coming, spend your money to protect the sacred fridge!
0,0,0,0,0,0,0,0,0,0
That wasn't that bad, was it? Well these next enemies ARE that bad, prepare yourself!
0,0,0,0,1,1,1,1,1
You're stronger than I thought. Let's see how you handle this next wave
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
I bet you're wondering what the police tower does. Maybe you should give them a try.
0,0,0,0,1,1,1,1,2,2
Hopefully you figured it out, donuts can only be eaten by police.Make sure you're ready for them.
2,2,2,2,1,1,1,1,1,1,1,1,1,1
It looked like you almost had a hard time with that last wave.Are you sure you're good enough to do this?
1,1,1,1,1,2,2,2,2,2,0,0,0,0,0,3
hAmBuRgEr!?...
1,1,1,2,2,2,4,4,4,4
Those fries are pretty speedy, eh? Make sure you have a fast firing tower, so you're sure to hit them.
3,3,4,4,4,4,4,4
Here's a real challenge, this wave is massive!(Your computer might not have enough ram for this)
4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4
Fast didn't work, maybe slow and steady protects the fridge.
3,3,3,3,3,3,3,3,3,3*/
