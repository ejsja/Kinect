using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Kinect;


namespace KinectHandTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Runtime nui;
        KinectSensor sensor;
        private WriteableBitmap colorBitmap;
        private byte[] colorPixels;
        Image[] img;
        int[] imgNum;
        int returnImgNum = -1;
        int answer;
        Image[] judge = new Image[2];
        Image[] question;
        Image engQuestion;

        int questionX = 150;
        int questionY = 10;
        string sOp;
        int questionNum;
        int answerNum;

        bool inImg = false;
        bool judgeOn = false;
        bool judgeOff = false;
        
        bool chkMath = false;
        bool chkEng = false;
        
        DepthImageFrame depthFrame;
        DepthImageFormat depthFormat;

        private void DeleteScreen()
        {            
            int i;
          
            if (chkMath)
            {
                for (i = 0; i < 6; i++)
                {
                    canvas.Children.Remove(img[i]);
                    img[i] = null;
                }

                for (i = 0; i < 5; i++)
                {
                    canvas.Children.Remove(question[i]);
                    question[i] = null;
                }
            }
            else if (chkEng)
            {
                for (i = 0; i < 4; i++)
                {
                    canvas.Children.Remove(img[i]);
                    img[i] = null;
                }
                canvas.Children.Remove(engQuestion);
                engQuestion = null;
            }
        }
        
        private void CreateMathQuestion()
        {
            Random r = new Random();
            //Create Question
            question = new Image[5];
            int i;
            for (i = 0; i < 5; i++)
                question[i] = new Image();

            string path = "../../Images/box.png";
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            question[0].Height = 100;
            question[0].Source = src;
            canvas.Children.Add(question[0]);

            int op = r.Next(0, 100);
            questionNum = r.Next(0, 9);
            answerNum = r.Next(0, 9);

            if (op % 2 == 1)
            {
                sOp = "-";
                if (questionNum + answerNum >= 10)
                {
                    questionNum = 9 - answerNum;
                }
                answer = answerNum + questionNum;
            }
            else
            {
                sOp = "+";
                if (questionNum > answerNum)
                {
                    int tmp = questionNum;
                    questionNum = answerNum;
                    answerNum = tmp;
                }
                answer = answerNum - questionNum;
            }
            path = "../../Images/" + sOp + ".png";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            question[1].Height = 100;
            question[1].Source = src;
            canvas.Children.Add(question[1]);

            path = "../../Images/" + questionNum + ".png";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            question[2].Height = 100;
            question[2].Source = src;
            canvas.Children.Add(question[2]);

            path = "../../Images/=.png";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            question[3].Height = 100;
            question[3].Source = src;
            canvas.Children.Add(question[3]);

            path = "../../Images/" + answerNum + ".png";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            question[4].Height = 100;
            question[4].Source = src;
            canvas.Children.Add(question[4]);
            
            Canvas.SetTop(question[0], questionY);
            Canvas.SetLeft(question[0], questionX);

            Canvas.SetTop(question[1], 10);
            Canvas.SetLeft(question[1], 230);

            Canvas.SetTop(question[2], 10);
            Canvas.SetLeft(question[2], 310);

            Canvas.SetTop(question[3], 10);
            Canvas.SetLeft(question[3], 390);

            Canvas.SetTop(question[4], 10);
            Canvas.SetLeft(question[4], 470);

            Random random = new Random();

            img = new Image[6];
            imgNum = new int[6];

            i = 0;
            while (i <= 4)
            {
                int num = r.Next(0, 9);
                int j = 0;
                for (j = 0; j < i; j++)
                {
                    if (imgNum[j] == num)
                        break;
                }

                if (j == i)
                    imgNum[i++] = num;
            }

            imgNum[i] = answer;

            for (i = 0; i < 6; i++)
            {
                path = @"../../Images/" + imgNum[i] + ".png";
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                img[i] = new Image();
                src = new BitmapImage();
                src.BeginInit();
                src.StreamSource = stream;
                src.EndInit();
                img[i].Height = 100;
                img[i].Source = src;
                canvas.Children.Add(img[i]);
                Canvas.SetTop(img[i], 10 + r.Next(140, 300));
                Canvas.SetLeft(img[i], r.Next(0, 500));
            }
        }

        private void CreateEnglishQuestion()
        {   
            Random r = new Random();
            //Create Question
            String[] questionName = new String[4];
            engQuestion = new Image();

            questionName[0] = "apple";
            questionName[1] = "banana";
            questionName[2] = "grape";
            questionName[3] = "strawberry";

            int questionNum = r.Next(0, 4);
            answer = questionNum;

            string path = "../../Images/" + questionName[questionNum] + ".png";

            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            engQuestion.Height = 200;
            engQuestion.Source = src;

            canvas.Children.Add(engQuestion);
            Canvas.SetTop(engQuestion, 50);
            Canvas.SetLeft(engQuestion, 100);
               
            img = new Image[4];
            
            for (int i = 0; i < 4; i++)
                img[i] = new Image();

            path = @"../../Images/apple.jpg";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            img[0].Height = 100;
            img[0].Source = src;

            path = @"../../Images/banana.jpg";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            img[1].Height = 100;
            img[1].Source = src;

            path = @"../../Images/grape.jpg";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            img[2].Height = 100;
            img[2].Source = src;

            path = @"../../Images/strawberry.jpg";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();
            img[3].Height = 100;
            img[3].Source = src;

            int[] arr = new int[4];
            
            Random random = new Random();
            int ran;
            int ranIdx = 0;
            bool ckRan = false;
            while (ranIdx != 4)
            {
                ran = random.Next(0, 4);

                for (int j = 0; j < ranIdx; j++)
                {
                    if (arr[j] == ran)
                    {
                        ckRan = true;
                        break;
                    }
                }

                if (!ckRan)
                {
                    arr[ranIdx] = ran;
                    ranIdx++;
                }

                ckRan = false;
            }

            for (int i = 0; i < 4; i++)
            {
                int num = arr[i];
                canvas.Children.Add(img[num]);
                Canvas.SetTop(img[num], 250);
                Canvas.SetLeft(img[num], (i + 0.2) * 160);
            }
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            var depthPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, depthFormat);
            double depthX = depthPoint.X;
            double depthY = depthPoint.Y;

            depthX = Math.Max(0, Math.Min(depthX * 320, 320));
            depthY = Math.Max(0, Math.Min(depthY * 240, 240));

            int colorX, colorY;
            colorX = colorY = 0;
            
            ColorImagePoint colorPoint = sensor.CoordinateMapper.MapSkeletonPointToColorPoint(skelpoint, sensor.ColorStream.Format);
            colorX = colorPoint.X;
            colorY = colorPoint.Y;
            
            return new Point((canvas.Width * colorX / 640.0), (canvas.Height * colorY / 480));
        }
         
        private void KinectAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            depthFrame = e.OpenDepthImageFrame();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
            
            depthFormat = sensor.DepthStream.Format;
            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];
            colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
            this.Image.Source = this.colorBitmap;
            sensor.ColorFrameReady += this.SensorColorFrameReady;

            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };

            sensor.SkeletonStream.Enable(parameters);
            sensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(nui_SkeletonFrameReady);

            try
            {
                sensor.Start();
                sensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
                depthFrame = sensor.DepthStream.OpenNextFrame(0); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        void nui_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            SkeletonFrame skeletonFrame = e.OpenSkeletonFrame();

            try
            {
                Skeleton[] test = new Skeleton[skeletonFrame.SkeletonArrayLength];  // this Error point. Null error
                skeletonFrame.CopySkeletonDataTo(test);
                foreach (Skeleton data in test)
                {   
                    if (SkeletonTrackingState.Tracked == data.TrackingState)
                    {
                        Point rightHandPosition, leftHandPosition, shoulderPosition;
                        Joint rightHandJoint = data.Joints[JointType.HandRight];
                        Joint leftHandJoint = data.Joints[JointType.HandLeft];
                        Joint shoulderCenter = data.Joints[JointType.ShoulderCenter];

                        rightHandPosition = SkeletonPointToScreen(rightHandJoint.Position);
                        leftHandPosition = SkeletonPointToScreen(leftHandJoint.Position);
                        shoulderPosition = SkeletonPointToScreen(shoulderCenter.Position);
                        
                        double leftHandX = leftHandPosition.X;
                        double leftHandY = leftHandPosition.Y;

                        double rightHandX = rightHandPosition.X;
                        double rightHandY = rightHandPosition.Y;

                        double differX = rightHandPosition.X - leftHandPosition.X;
                        double differY = leftHandPosition.Y - rightHandPosition.Y;

                        double shoulderY = shoulderPosition.Y;

                        if (chkMath)
                        {
                            if (judgeOn)        // 정답 혹은 오답 화면이 떠 있는 경우
                            {
                                if (((shoulderPosition.Y - leftHandPosition.Y) > 50) && ((shoulderPosition.Y - rightHandPosition.Y) > 50))
                                {
                                    canvas.Children.Remove(judge[0]);
                                    judgeOn = false;

                                    DeleteScreen();
                                    CreateMathQuestion();
                                }
                            }
                            else if (judgeOff)
                            {
                                if (((shoulderPosition.Y - leftHandPosition.Y) > 50) && ((shoulderPosition.Y - rightHandPosition.Y) > 50))
                                {
                                    canvas.Children.Remove(judge[1]);
                                    judgeOff = false;

                                    Canvas.SetTop(img[returnImgNum], 400);
                                    Canvas.SetLeft(img[returnImgNum], 400);
                                    canvas.Children.Remove(img[returnImgNum]);
                                }
                            }
                            else
                            {
                                if (differX < 100 && differY < 100)
                                {
                                    if (!inImg)
                                    {
                                        if (leftHandPosition.Y < rightHandPosition.Y)
                                            returnImgNum = SuccessHandPosition(leftHandPosition.Y + 50, leftHandPosition.X + 30, 100, 100, 6);
                                        else
                                            returnImgNum = SuccessHandPosition(leftHandPosition.Y + 50, leftHandPosition.X + 30, 100, -100, 6); // 수정 요망

                                        inImg = true;
                                    }

                                    if (returnImgNum == -1)
                                    {
                                        inImg = false;
                                        break;
                                    }

                                    Canvas.SetLeft(img[returnImgNum], leftHandPosition.X - 30);
                                    Canvas.SetTop(img[returnImgNum], leftHandPosition.Y - 50);

                             }
                             else
                             {
                                 if (returnImgNum == -1)
                                     break;

                                 if ((questionX < Canvas.GetLeft(img[returnImgNum]) + 100) && (Canvas.GetLeft(img[returnImgNum]) + 100 < questionX + 150))
                                 {
                                     if (questionY < (Canvas.GetTop(img[returnImgNum]) + 50) && (Canvas.GetTop(img[returnImgNum]) + 50) < questionY + 150)
                                     {
                                         for (int i = 0; i < 2; i++)
                                             judge[i] = new Image();

                                         string path = "../../Images/success.png";
                                         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                                         BitmapImage src = new BitmapImage();
                                         src.BeginInit();
                                         src.StreamSource = stream;
                                         src.EndInit();

                                         if (answer == imgNum[returnImgNum])
                                         {
                                             judge[0].Height = 400;
                                             judge[0].Source = src;
                                             canvas.Children.Add(judge[0]);
                                             Canvas.SetLeft(judge[0], 80);
                                             Canvas.SetTop(judge[0], 0);

                                             judgeOn = true;
                                         }
                                         else
                                         {
                                             path = "../../Images/fail.png";
                                             stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                                             src = new BitmapImage();
                                             src.BeginInit();
                                             src.StreamSource = stream;
                                             src.EndInit();
                                             judge[1].Height = 400;
                                             judge[1].Source = src;
                                             canvas.Children.Add(judge[1]);

                                             Canvas.SetLeft(judge[1], 50);
                                             Canvas.SetTop(judge[1], 0);

                                             judgeOff = true;
                                         }
                                     }
                                     inImg = false;
                                 }
                              }
                            }
                        }
                        else if(chkEng)
                        {
                            int returnImgNum = -1;
                            
                            
                            if (judgeOn)
                            {
                                if (((shoulderPosition.Y - leftHandPosition.Y) > 50) && ((shoulderPosition.Y - rightHandPosition.Y) > 50))
                                {
                                    canvas.Children.Remove(judge[0]);
                                    judgeOn = false;

                                    DeleteScreen();
                                    CreateEnglishQuestion();
                                }
                            }
                            else if (judgeOff)
                            {
                                if (((shoulderPosition.Y - leftHandPosition.Y) > 50) && ((shoulderPosition.Y - rightHandPosition.Y) > 50))
                                {
                                    canvas.Children.Remove(judge[1]);
                                    judgeOff = false;
                                }
                            }
                            else
                            {
                                if (differX < 100 && differY < 100)
                                {
                                    for (int i = 0; i < 2; i++)
                                        judge[i] = new Image();
                                    string path = "../../Images/success.png";
                                    FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                                    BitmapImage src = new BitmapImage();
                                    if (!inImg)
                                    {
                                        if (leftHandPosition.Y < rightHandPosition.Y)
                                            returnImgNum = SuccessHandPosition(leftHandPosition.Y + 50, leftHandPosition.X + 30, 100, 100, 4);
                                        else
                                            returnImgNum = SuccessHandPosition(leftHandPosition.Y + 50, leftHandPosition.X + 30, 100, -100, 4); // 수정 요망

                                        Console.WriteLine("{0}, {1}", returnImgNum, answer);


                                        if (answer == returnImgNum)
                                        {

                                            src.BeginInit();
                                            src.StreamSource = stream;
                                            src.EndInit();
                                            judge[0].Height = 400;
                                            judge[0].Source = src;
                                            canvas.Children.Add(judge[0]);
                                            Canvas.SetLeft(judge[0], 80);
                                            Canvas.SetTop(judge[0], 0);
                                            judgeOn = true;
                                        }
                                        else
                                        {

                                            path = "../../Images/fail.png";
                                            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                                            src = new BitmapImage();
                                            src.BeginInit();
                                            src.StreamSource = stream;
                                            src.EndInit();
                                            judge[1].Height = 400;
                                            judge[1].Source = src;
                                            canvas.Children.Add(judge[1]);

                                            Canvas.SetLeft(judge[1], 50);
                                            Canvas.SetTop(judge[1], 0);
                                            judgeOff = true;
                                        }
                                    }
                                }
                            }
                        }
                    } // END IF   
                }
            }
            catch (Exception error)
            {
                //MessageBox.Show("error");
            }
        }


        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    // Copy the pixel data from the image to a temporary array
                    colorFrame.CopyPixelDataTo(this.colorPixels);

                    // Write the pixel data into our bitmap
                    this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        this.colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int), 0);
                }
            }
        }

        private int SuccessHandPosition(double top, double left, double width, double height, int MaxImgNum)
        {
            int i, result = -1 ;
            for (i = 0; i < MaxImgNum; i++)
            {
                double imgTop = Canvas.GetTop(img[i]);
                double imgLeft = Canvas.GetLeft(img[i]);
                
                bool check = (imgTop <= top  && top <= imgTop+100) && (imgLeft <= left && left <= imgLeft + 100);
                if (check)
                {
                    return i;
                }
            }
            return result;
        }

        private void Window_Closed(object sender, EventArgs e)
        {            
            sensor.Stop();
        }

        private void MathButton_Click(object sender, RoutedEventArgs e)
        {   
            if (chkMath || chkEng)
            {
                DeleteScreen();
            }

            CreateMathQuestion();
            chkMath = true;
        }

        private void EngButton_Click(object sender, RoutedEventArgs e)
        {
            if (chkMath || chkEng)
            {
                DeleteScreen();
            }

            CreateEnglishQuestion();
            chkEng = true;
        }
    }
}