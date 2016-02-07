using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace TravelingWave
    {
        class TravelingWaveM
        {
            public Prog1 form;
            private bool deltaCoupl;
            private int n, m;
            private double hx, ht;
            private double l, TB;
            private double[] x, t;

            private double[,] u;
            private double alpha, beta, gamma;
            private double a;
            private double b, d;
            private double iExt;
            public int N
            {   
                get { return this.n; }
                set { this.n = value; }
            }

            public int M
            {   
                get { return this.m; }
                set { this.m = value; }
            }

            public double L
            {  
                get { return this.l; }
                set { this.l = value; }
            }

            public double T
            {   // t's segment
                get { return this.TB; }
                set { this.TB = value; }
            }

            public double Alpha
            {
                get { return this.alpha; }
                set { this.alpha = value; }
            }

            public double Beta
            {
                get { return this.beta; }
                set { this.beta = value; }
            }

            public double Gamma
            {
                get { return this.gamma; }
                set { this.gamma = value; }
            }

            public double A
            {   // f's constant
                get { return this.a; }
                set { this.a = value; }
            }

            public double B
            {
                get { return this.b; }
                set { this.b = value; }
            }

            public double D
            {
                get { return this.d; }
                set { this.d = value; }
            }

            public double I
            {   // current
                get { return this.iExt; }
                set { this.iExt = value; }
            }

            public bool Eq
            {
                get { return this.deltaCoupl; }
                set { this.deltaCoupl = value; }
            }

   
            public TravelingWaveM(double alpha, double beta, double gamma, double a, double b, double d, double l, double TB, double iExt, int m, int n, bool deltaCoupl, Form1 form)
            {
                this.alpha = alpha;
                this.beta = beta;
                this.gamma = gamma;
                this.a = a;
                this.b = b;
                this.d = d;
                this.l = l;
                this.TB = TB;
                this.iExt = iExt;
                this.n = n;
                this.m = m;
                this.deltaCoupl = deltaCoupl;
                this.form = form;

            }

          
            public void load(int m, int n, double l, double TB)
            {
                this.n = n;
                this.m = m;

                this.hx = 2 * l / n;
                this.ht = TB / m;

                this.x = new double[n + 1];
                for (int i = 0; i < n + 1; i++) this.x[i] = -this.l + i * this.hx;

                this.t = new double[m + 1];
                for (int j = 0; j < m + 1; j++) this.t[j] = j * this.ht;

                this.u = new double[m + 1, n + 1];
            }

            public void initials(int n)
            {   

                for (int i = 0; i < n + 1; i++)
                {  
                    this.u[0, i] = u_x_0(x[i]);
                }
            }

        public void solve()
            {
                int prBarMax = 10;
                form.prBarSolve.Maximum = prBarMax;

                int i, j, k;

                k = Convert.ToInt32(this.d / this.hx);
                this.d = this.hx * k;
                form.txtBoxD.Text = Convert.ToString(this.d);

                double step = this.ht / (this.hx * this.hx);

                for (i = 0; i < this.n; i++)
                {
                    u[0 , i] = (1 / 2) * (1 + Math.Tanh(x / (2 * Math.Sqrt(2))));
                }
                for (i = 1; i < this.n; i++)
                {
                    for (j = 0; j < this.m; j++)
                    { 
                        u[i, j + 1] = u[i, j] + Math.tau(D * ((u[i + 1, j] - 2 * u[i, j] + u[i - 1, j]) / (h * h)) - u[i, j] * (u[i, j] - alpha)(u[i, j] - 1) + b((1 / 2) * (u[i + k, j] + u[i - k, i]) - u[i, j])); 
                    }

                    if (form.prBarSolve.Value < prBarMax) 
                        form.prBarSolve.Value = prBarMax;
                }
        }

        public double getX(int i)
            {
                return this.x[i];
            }

            public double getT(int j)
            {
                return this.t[j];
            }

            public double getU(int j, int i)
            {
                return this.u[j, i];
            }

            private double u_x_0(double x)
            {   // initial u wave at t = 0
                double u0 = -1.199;
                if (x < -40) return 1.0;
                else if ((x >= -40) && (x <= -30))
                    return (u0 - 1) * (x + 30) / 10 + u0;
                else
                    return u0;

                //PUx0.Evaluate(SUx0.Replace("x", x.ToString()));
                //return PUx0.Result;

                //return Math.Exp(-x * x / 2) / (2 * Math.PI);

                //return 1.0 / 2 * Math.Exp(-Math.Abs(x + 2));
            }


            private double u_0_t(double t) { return 0.0; } // Neumann boundary condition at x = -l

            private double u_l_t(double t) { return 0.0; } // Neumann boundary condition at x = l
        }
    }
