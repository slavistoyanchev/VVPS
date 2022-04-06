using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//конвенция класовете в Паскал Кейс 
//class minite -> class Minite; class tochki-> class Tochki
//променливите  в Камел Кейс 
//име -> name ; то4ки->dots; to4kii->points ; Редове->Rows ; Колони->Colums ; игрално_поле->playingField
//аргументи-> args ; slojibombite-> slojiBombite
//create_igralno_pole()->createIgralnoPole()
namespace mini4ki
{
	public class Minite
	{
		public class Tochki
		{
			string name;
			int dots;

			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public int Dots
			{
				get { return dots; }
				set { fots = value; }
			}

			public Tochki() { }

			public Tochki(string name, int dots)
			{
				this.name = name;
				this.dots = dots;
			}
		}

		static void Main(string[] args)
		{
			string komanda = string.Empty;
			char[,] poleto = createIgralnoPole();
			char[,] bombite = slojiBombite();
			int broya4 = 0;
			bool grum = false;
			List<Tochki> shampion4eta = new List<Tochki>(6);
			int red = 0;
			int kolona = 0;
			bool flag = true;
			const int maks = 35;
			bool flag2 = false;

			do
			{
				if (flag)
				{
					Console.WriteLine("Хайде да играем на “Mini4KI”. Пробвай си късмета да намерите местата без минички." +
					" Команда 'top' показва класирането, 'restart' почва нова игра, 'exit' излиза от играта!");
					dumpp(poleto);
					flag = false;
				}
				Console.Write("Дайте ред и колона : ");
				komanda = Console.ReadLine().Trim();
				if (komanda.Length >= 3)
				{
					if (int.TryParse(komanda[0].ToString(), out red) &&
					int.TryParse(komanda[2].ToString(), out kolona) &&
						red <= poleto.GetLength(0) && kolona <= poleto.GetLength(1))
					{
						komanda = "turn";
					}
				}
				switch (komanda)
				{
					case "top":
						klasacia(shampion4eta);
						break;
					case "restart":
						poleto = createIgralnoPole();
						bombite = slojiBombite();
						dumpp(poleto);
						grum = false;
						flag = false;
						break;
					case "exit":
						Console.WriteLine("Довиждане!");
						break;
					case "turn":
						if (bombite[red, kolona] != '*')
						{
							if (bombite[red, kolona] == '-')
							{
								tiSiNaHod(poleto, bombite, red, kolona);
								broya4++;
							}
							if (maks == broya4)
							{
								flag2 = true;
							}
							else
							{
								dumpp(poleto);
							}
						}
						else
						{
							grum = true;
						}
						break;
					default:
						Console.WriteLine("\nГрешка! Невалидна команда!\n");
						break;
				}
				if (grum)
				{
					dumpp(bombite);
					Console.Write("\n Ямря геройски с {0} точки. " +
						"Постави никнейм: ", broya4);
					string niknejm = Console.ReadLine();
					Tochki t = new Tochki(niknejm, broya4);
					if (shampion4eta.Count < 5)
					{
						shampion4eta.Add(t);
					}
					else
					{
						for (int i = 0; i < shampion4eta.Count; i++)
						{
							if (shampion4eta[i].То4ки < t.То4ки)
							{
								shampion4eta.Insert(i, t);
								shampion4eta.RemoveAt(shampion4eta.Count - 1);
								break;
							}
						}
					}
					shampion4eta.Sort((Tochki r1, Tochki r2) => r2.Име.CompareTo(r1.Име));
					shampion4eta.Sort((Tochki r1, Tochki r2) => r2.То4ки.CompareTo(r1.То4ки));
					klasacia(shampion4eta);

					poleto = createIgralnoPole();
					bombite = slojiBombite();
					broya4 = 0;
					grum = false;
					flag = true;
				}
				if (flag2)
				{
					Console.WriteLine("\nBRAVO! Отвори 35 клетки.");
					dumpp(bombite);
					Console.WriteLine("Дайте никнейм: ");
					string imeee = Console.ReadLine();
					Tochki points = new Tochki(imeee, broya4);
					shampion4eta.Add(points);
					klasacia(shampion4eta);
					poleto = createIgralnoPole();
					bombite = slojiBombite();
					broya4 = 0;
					flag2 = false;
					flag = true;
				}
			}
			while (komanda != "exit");
			Console.WriteLine("Made in Bulgaria !");
			//Console.WriteLine("AREEEEEEeeeeeee.");
			Console.Read();
		}

		private static void klasacia(List<Tochki> points)
		{
			Console.WriteLine("\nТочки:");
			if (points.Count > 0)
			{
				for (int i = 0; i < points.Count; i++)
				{
					Console.WriteLine("{0}. {1} --> {2} кутии",
						i + 1, points[i].Name, points[i].Dots);
				}
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine("Празна класация!\n");
			}
		}

		private static void tiSiNaHod(char[,] POLE,
			char[,] BOMBI, int RED, int KOLONA)
		{
			char kolkoBombi = kolko(BOMBI, RED, KOLONA);
			BOMBI[RED, KOLONA] = kolkoBombi;
			POLE[RED, KOLONA] = kolkoBombi;
		}

		private static void dumpp(char[,] board)
		{
			int RRR = board.GetLength(0);
			int KKK = board.GetLength(1);
			Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
			Console.WriteLine("   ---------------------");
			for (int i = 0; i < RRR; i++)
			{
				Console.Write("{0} | ", i);
				for (int j = 0; j < KKK; j++)
				{
					Console.Write(string.Format("{0} ", board[i, j]));
				}
				Console.Write("|");
				Console.WriteLine();
			}
			Console.WriteLine("   ---------------------\n");
		}

		private static char[,] createIgralnoPole()
		{
			int boardRows = 5;
			int boardColumns = 10;
			char[,] board = new char[boardRows, boardColumns];
			for (int i = 0; i < boardRows; i++)
			{
				for (int j = 0; j < boardColumns; j++)
				{
					board[i, j] = '?';
				}
			}

			return board;
		}

		private static char[,] slojiBombite()
		{
			int Rows = 5;
			int Colums = 10;
			char[,] playingField = new char[Rows, Colums];

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Colums; j++)
				{
					playingField[i, j] = '-';
				}
			}

			List<int> r3 = new List<int>();
			while (r3.Count < 15)
			{
				Random random = new Random();
				int asfd = random.Next(50);
				if (!r3.Contains(asfd))
				{
					r3.Add(asfd);
				}
			}

			foreach (int i2 in r3)
			{
				int kol = (i2 / Colums);
				int red = (i2 % Colums);
				if (red == 0 && i2 != 0)
				{
					kol--;
					red = Colums;
				}
				else
				{
					red++;
				}
				playingField[kol, red - 1] = '*';
			}

			return playingField;
		}

		private static void smetki(char[,] pole)
		{
			int kol = pole.GetLength(0);
			int red = pole.GetLength(1);

			for (int i = 0; i < kol; i++)
			{
				for (int j = 0; j < red; j++)
				{
					if (pole[i, j] != '*')
					{
						char kolkoo = kolko(pole, i, j);
						pole[i, j] = kolkoo;
					}
				}
			}
		}

		private static char kolko(char[,] r, int rr, int rrr)
		{
			int brojkata = 0;
			int reds = r.GetLength(0);
			int kols = r.GetLength(1);

			if (rr - 1 >= 0)
			{
				if (r[rr - 1, rrr] == '*')
				{ 
					brojkata++; 
				}
			}
			if (rr + 1 < reds)
			{
				if (r[rr + 1, rrr] == '*')
				{ 
					brojkata++; 
				}
			}
			if (rrr - 1 >= 0)
			{
				if (r[rr, rrr - 1] == '*')
				{ 
					brojkata++;
				}
			}
			if (rrr + 1 < kols)
			{
				if (r[rr, rrr + 1] == '*')
				{ 
					brojkata++;
				}
			}
			if ((rr - 1 >= 0) && (rrr - 1 >= 0))
			{
				if (r[rr - 1, rrr - 1] == '*')
				{ 
					brojkata++; 
				}
			}
			if ((rr - 1 >= 0) && (rrr + 1 < kols))
			{
				if (r[rr - 1, rrr + 1] == '*')
				{ 
					brojkata++; 
				}
			}
			if ((rr + 1 < reds) && (rrr - 1 >= 0))
			{
				if (r[rr + 1, rrr - 1] == '*')
				{ 
					brojkata++; 
				}
			}
			if ((rr + 1 < reds) && (rrr + 1 < kols))
			{
				if (r[rr + 1, rrr + 1] == '*')
				{ 
					brojkata++; 
				}
			}
			return char.Parse(brojkata.ToString());
		}
	}
}
