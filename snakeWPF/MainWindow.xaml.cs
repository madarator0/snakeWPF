using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace snakeWPF
{
    public partial class MainWindow : Window
    {
        private LabelV2[,] labels;
        private DispatcherTimer gameTimer;
        private XY snakeHead;
        private XY food;
        private List<XY> snakeBody;
        private string direction;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeGame();
            this.KeyDown += new KeyEventHandler(OnKeyDown);
        }

        private void InitializeGrid()
        {
            labels = new LabelV2[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    labels[row, col] = new LabelV2();
                    Grid.SetRow(labels[row, col], row);
                    Grid.SetColumn(labels[row, col], col);
                    mainG.Children.Add(labels[row, col]);
                }
            }
        }

        private void InitializeGame()
        {
            snakeHead = new XY(5, 5);
            snakeBody = new List<XY> { snakeHead };
            direction = "Right";
            GenerateFood();

            gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            gameTimer.Tick += GameTick;
            gameTimer.Start();

            Draw();
        }

        private void GenerateFood()
        {
            Random rand = new Random();
            do
            {
                food = new XY(rand.Next(0, 10), rand.Next(0, 10));
            } while (snakeBody.Contains(food));
        }

        private void GameTick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            Draw();
        }

        private void MoveSnake()
        {
            XY newHead = new XY(snakeHead.X, snakeHead.Y);

            switch (direction)
            {
                case "Up":
                    newHead.Y -= 1;
                    break;
                case "Down":
                    newHead.Y += 1;
                    break;
                case "Left":
                    newHead.X -= 1;
                    break;
                case "Right":
                    newHead.X += 1;
                    break;
            }

            snakeBody.Insert(0, newHead);
            snakeHead = newHead;

            if (snakeHead.X == food.X && snakeHead.Y == food.Y)
            {
                GenerateFood();
            }
            else
            {
                snakeBody.RemoveAt(snakeBody.Count - 1);
            }
        }

        private void CheckCollision()
        {
            if (snakeHead.X < 0 || snakeHead.X >= 10 || snakeHead.Y < 0 || snakeHead.Y >= 10)
            {
                gameTimer.Stop();
                MessageBox.Show("Game Over");
            }

            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeHead.X == snakeBody[i].X && snakeHead.Y == snakeBody[i].Y)
                {
                    gameTimer.Stop();
                    MessageBox.Show("Game Over");
                    break;
                }
            }
        }

        private void Draw()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    labels[row, col].Background = Brushes.White;
                    labels[row, col].CornerRadius = new CornerRadius(0);
                }
            }

            labels[snakeBody[0].Y, snakeBody[0].X].Background = Brushes.Green;
            labels[snakeBody[0].Y, snakeBody[0].X].CornerRadius = new CornerRadius(100);

            foreach (var part in snakeBody.Skip(1))
            {
                labels[part.Y, part.X].Background = Brushes.LightGreen;
            }

            labels[food.Y, food.X].Background = Brushes.Red;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if (direction != "Down")
                        direction = "Up";
                    break;
                case Key.S:
                    if (direction != "Up")
                        direction = "Down";
                    break;
                case Key.A:
                    if (direction != "Right")
                        direction = "Left";
                    break;
                case Key.D:
                    if (direction != "Left")
                        direction = "Right";
                    break;
            }
        }
    }
}