using System;

namespace Sudoku
{
	class Program
	{

		
		private const int TABLE_SIZE = 9;
		private int[,] table = {
			{ 8, 0, 0,    0, 0, 0,    0, 0, 0 },
			{ 0, 0, 3,    6, 0, 0,    0, 0, 0 },
			{ 0, 7, 0,    0, 9, 0,    2, 0, 0 },

			{ 0, 5, 0,    0, 0, 7,    0, 0, 0 },
			{ 0, 0, 0,    0, 4, 5,    7, 0, 0 },
			{ 0, 0, 0,    1, 0, 0,    0, 3, 0 },

			{ 0, 0, 1,    0, 0, 0,    0, 6, 8 },
			{ 0, 0, 8,    5, 0, 0,    0, 1, 0 },
			{ 0, 9, 0,    0, 0, 0,    4, 0, 0 },
		};

		public Program()
		{
			Solved = false;
		}

		private void PrintTable()
		{
			for (int i = 0; i < TABLE_SIZE; i++)
			{
				for (int j = 0; j < TABLE_SIZE; j++)
				{
					Console.Write(table[i, j].ToString() + " ");
					if (j % 3 == 2)
					{
						Console.Write(" ");
					}
				}
				Console.WriteLine();
				if (i % 3 == 2)
				{
					Console.WriteLine();
				}
			}
		}

		private bool NoContradiction()
		{
			int x = 0;

			for (int i = 0; i < TABLE_SIZE; i++)
			{
				x = 0;

				for (int j = 0; j < TABLE_SIZE; j++)
				{
					if (table[i, j] != 0) 
					{
						if ((x & (1 << table[i, j])) == 0)
						{
							x |= 1 << table[i, j];
						}
						else
						{
							return false;
						}
					}
				}
			}


			for (int i = 0; i < TABLE_SIZE; i++)
			{
				x = 0;

				for (int j = 0; j < TABLE_SIZE; j++)
				{
					if (table[j, i] != 0)
					{
						if ((x & (1 << table[j, i])) == 0)
						{
							x |= 1 << table[j, i];
						}
						else
						{
							return false;
						}
					}
				}
			}

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					x = 0;

					for (int k = 0; k < 3; k++)
					{
						for (int l = 0; l < 3; l++)
						{
							int col = i * 3 + k;
							int row = j * 3 + l;
							if (table[col, row] != 0)
							{
								if ((x & (1 << table[col, row])) == 0)
								{
									x |= 1 << table[col, row];
								}
								else
								{
									return false;
								}
							}
						}
					}
				}
			}

			return true;
		}

		public bool Solved { get; set; }

		public bool RemainingCellExists()
		{
			bool remainingCellExists = false;

			for (int i = 0; i < TABLE_SIZE; i++)
			{
				for (int j = 0; j < TABLE_SIZE; j++)
				{
					if (table[i, j] == 0)
					{
						remainingCellExists = true;
						break;
					}
				}

				if (remainingCellExists)
				{
					break;
				}
			}

			return remainingCellExists;
		}

		private void Solve()
		{
		//	PrintTable();

			if (!RemainingCellExists() && NoContradiction())
			{
				PrintTable();
				Solved = true;
				return;
			}

			for (int i = 0; i < TABLE_SIZE; i++)
			{
				for (int j = 0; j < TABLE_SIZE; j++)
				{
					#region Backtracing
					if (table[i, j] == 0)
					{
						for (int n = 1; n <= 9; n++)
						{
							table[i, j] = n;

							if (NoContradiction())
							{
								// keep on solving.
								Solve();
								if (Solved)
								{
									return;
								}
							}
						}

						table[i, j] = 0;
						return;
					}
					#endregion
				}
			}
		}

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Solve();
			Console.ReadLine();
		}
	}
}
