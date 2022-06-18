using System;
using System.Collections.Generic;
using System.Text;

namespace DFS_And_BFS
{
    public class Minesweeper
    {
        //dfs approch
        //
        int m, n;
        int[,] dirction;
        public char[][] UpdateBoard(char[][] board, int[] click)
        {
            if (board == null || board.Length == 0) return board;

            m = board.Length;
            n = board[0].Length;

            dirction = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };

            //BFS approch
            Queue<int[]> queue = new Queue<int[]>();

            if (board[click[0]][click[1]] == 'M')
            {
                board[click[0]][click[1]] = 'X';
                return board;
            }

            queue.Enqueue(new int[] { click[0], click[1] });
            board[click[0]][click[1]] = 'B';
            while (queue.Count != 0)
            {
                int[] curr = queue.Dequeue();
                int count = GetMineCount(board, curr[0], curr[1]);

                if (count > 0)
                {
                    board[curr[0]][curr[1]] = (char)(count + '0');
                }
                else
                {
                    for (int i = 0; i < dirction.GetLength(0); i++)
                    {
                        int nr = curr[0] + dirction[i, 0];
                        int nc = curr[1] + dirction[i, 1];

                        if (nr >= 0 && nr < m && nc >= 0 && nc < n && board[nr][nc] == 'E')
                        {

                            board[nr][nc] = 'B';
                            queue.Enqueue(new int[] { nr, nc });

                        }
                    }
                }
            }

            //DFS approch
            //dfs(board, click);

            return board;
        }


        private void dfs(char[][] board, int[] click)
        {
            //base
            if (click[0] < 0 || click[0] >= m || click[1] < 0 || click[1] >= n || board[click[0]][click[1]] != 'E')
                return;

            //logic
            board[click[0]][click[1]] = 'B';
            int count = GetMineCount(board, click[0], click[1]);
            if (count > 0)
            {
                board[click[0]][click[1]] = (char)(count + '0');
            }
            else
            {
                for (int i = 0; i < dirction.GetLength(0); i++)
                {
                    int nr = click[0] + dirction[i, 0];
                    int nc = click[1] + dirction[i, 1];
                    dfs(board, new int[] { nr, nc });
                }
            }

        }


        private int GetMineCount(char[][] board, int r, int c)
        {
            int count = 0;

            for (int i = 0; i < dirction.GetLength(0); i++)
            {
                int newrow = r + dirction[i, 0];
                int newcol = c + dirction[i, 1];

                if (newrow >= 0 && newrow < m && newcol >= 0 && newcol < n)
                {
                    if (board[newrow][newcol] == 'M')
                    {
                        count++;
                    }
                }
            }
            //Console.WriteLine(count);
            return count;
        }
    }
}
