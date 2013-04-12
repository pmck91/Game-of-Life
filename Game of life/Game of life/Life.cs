using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_life
{
    class Life
    {
        Cell[,] grid;
        Cell[,] newGrid;
        int _width;
        int _height;
        Texture2D tex;
        float _tick;
        float _elapsedTime = 0;
        bool _pause = false;

        public Life(int width, int height, float tick)
        {
            if (width <= 10 || height <= 10) throw new ArgumentOutOfRangeException("You need to give life a chance choose bigger numbers");

            grid = new Cell[height,width];
            newGrid = new Cell[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = new Cell();
                    newGrid[i, j] = new Cell();
                }
            }

            _width = width - 1;
            _height = height - 1;
            _tick = tick;
        }

        public void loadContent(GraphicsDevice gd)
        {
            tex = new Texture2D(gd, 1, 1);
            tex.SetData(new Color[] { Color.White });
        }

        public void update(GameTime gt)
        {
            if (!_pause)
            {
                _elapsedTime += gt.ElapsedGameTime.Milliseconds;

                if (_elapsedTime > _tick)
                {

                    #region left col
                    // left column case
                    for (int i = 1; i < _height - 1; i++)
                    {
                        var friends = 0;

                        if (grid[i - 1, 0].happilyAlive()) friends++;
                        if (grid[i + 1, 0].happilyAlive()) friends++;
                        if (grid[i - 1, 1].happilyAlive()) friends++;
                        if (grid[i, 1].happilyAlive()) friends++;
                        if (grid[i + 1, 1].happilyAlive()) friends++;

                        newGrid[i, 0].playGod(friends);

                    }
                    #endregion

                    #region right col
                    // right column case
                    for (int i = 1; i < _height - 1; i++)
                    {
                        var friends = 0;

                        if (grid[i - 1, _width].happilyAlive()) friends++;
                        if (grid[i + 1, _width].happilyAlive()) friends++;
                        if (grid[i - 1, _width - 1].happilyAlive()) friends++;
                        if (grid[i, _width - 1].happilyAlive()) friends++;
                        if (grid[i + 1, _width - 1].happilyAlive()) friends++;

                        newGrid[i, _width].playGod(friends);

                    }
                    #endregion

                    #region top row
                    // top row case
                    for (int i = 1; i < _width - 1; i++)
                    {
                        var friends = 0;

                        if (grid[0, i - 1].happilyAlive()) friends++;
                        if (grid[0, i + 1].happilyAlive()) friends++;
                        if (grid[1, i - 1].happilyAlive()) friends++;
                        if (grid[1, i].happilyAlive()) friends++;
                        if (grid[1, i + 1].happilyAlive()) friends++;

                        newGrid[0, i].playGod(friends);

                    }
                    #endregion

                    #region bottom row
                    // bottom row case
                    for (int i = 1; i < _width - 1; i++)
                    {
                        var friends = 0;

                        if (grid[_height, i - 1].happilyAlive()) friends++;
                        if (grid[_height, i + 1].happilyAlive()) friends++;
                        if (grid[_height - 1, i - 1].happilyAlive()) friends++;
                        if (grid[_height - 1, i].happilyAlive()) friends++;
                        if (grid[_height - 1, i + 1].happilyAlive()) friends++;

                        newGrid[_height, i].playGod(friends);

                    }
                    #endregion

                    #region centre
                    // centre row & col case
                    for (int i = 1; i < _height - 1; i++)
                    {
                        var friends = 0;
                        for (int j = 1; j < _width - 1; j++)
                        {
                            if (grid[i - 1, j - 1].happilyAlive()) friends++;
                            if (grid[i - 1, j].happilyAlive()) friends++;
                            if (grid[i - 1, j + 1].happilyAlive()) friends++;

                            if (grid[i, j - 1].happilyAlive()) friends++;
                            //active cell fits here
                            if (grid[i, j + 1].happilyAlive()) friends++;

                            if (grid[i + 1, j - 1].happilyAlive()) friends++;
                            if (grid[i + 1, j].happilyAlive()) friends++;
                            if (grid[i + 1, j + 1].happilyAlive()) friends++;

                            if (friends == 3 && !grid[i,j].happilyAlive())
                            {
                                var aaaa = 0;
                            }

                            newGrid[i, j] = grid[i, j];
                            newGrid[i, j].playGod(friends);
                        }
                    }
                    #endregion

                    #region corners
                    {
                        var friends = 0;
                        // top left
                        if (grid[0, 1].happilyAlive()) friends++;
                        if (grid[1, 1].happilyAlive()) friends++;
                        if (grid[1, 0].happilyAlive()) friends++;

                        newGrid[0, 0].playGod(friends);

                        // top right
                        friends = 0;

                        if (grid[0, _width - 1].happilyAlive()) friends++;
                        if (grid[1, _width - 1].happilyAlive()) friends++;
                        if (grid[1, _width].happilyAlive()) friends++;

                        newGrid[0, _width].playGod(friends);

                        // bottom left
                        friends = 0;

                        if (grid[_height, 1].happilyAlive()) friends++;
                        if (grid[_height - 1, 1].happilyAlive()) friends++;
                        if (grid[_height - 1, 0].happilyAlive()) friends++;

                        newGrid[_height, 0].playGod(friends);

                        // bottom right
                        friends = 0;

                        if (grid[_height, _width - 1].happilyAlive()) friends++;
                        if (grid[_height - 1, _width - 1].happilyAlive()) friends++;
                        if (grid[_height - 1, _width].happilyAlive()) friends++;

                        newGrid[_height, _width].playGod(friends);

                    }
                    #endregion

                    grid = newGrid;

                //    purgeNewGrid();

                    _elapsedTime = 0f;
                }
            }
        }

        public void draw(SpriteBatch sb)
        {
            var cellWidth = 500 / (_width +1);
            var cellHeight = 500 / (_height+1);

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (i == 2 && j == 1)
                    {
                        var a = 'a';
                    }
                    sb.Draw(tex, new Rectangle((j* cellWidth) + 10, (i*  cellHeight) + 10, cellWidth, cellHeight) , grid[i, j].health());
                }
            }
        }

        public void seed()
        {
            // defauls seed is 8% of cells are alive
            seedByAmount((((_width+1) * (_height+1)) / 100) * 8);
        }

        public void seedByAmount(int lifeAmount)
        {
            if (lifeAmount <= 0 || lifeAmount > ((_width+1)*(_height+1))) throw new ArgumentOutOfRangeException("must be a number from 1 to width*height");

            Random x = new Random(DateTime.Now.Millisecond);
            Random y = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < lifeAmount; i++)
            {
                var yLoc = y.Next(0, _height);
                var xloc = x.Next(0, _width);
                grid[yLoc,xloc].revive();
                grid[yLoc, xloc].setColour(Color.Purple);
            }
        }

        public void seedByPercent(int lifePercent)
        {
            if (lifePercent <= 0 || lifePercent > 100) throw new ArgumentOutOfRangeException("must be a percent from 1 to 100");

            seedByAmount((((_width + 1) * (_height + 1)) / 100) * lifePercent);
        }

        public void setTick(float tick)
        {
            _tick = tick;
        }

        public void togglePause()
        {
            _pause = !_pause;
        }

        private void purgeNewGrid()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    newGrid[i, j] = new Cell();
                }
            }
        }
    }
}
