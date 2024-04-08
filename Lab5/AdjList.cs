using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class AdjList
    {
        LinkedList<int>[] v;
        int n;  // Số đỉnh
        //Propeties
        public int N { get => n; set => n = value; }
        public LinkedList<int>[] V
        {
            get { return v; }
            set { v = value; }
        }
        // Contructor
        public AdjList() { }
        public AdjList(int k)   // Khởi tạo v có k đỉnh
        {
            v = new LinkedList<int>[k];
        }
        // copy g --> đồ thị hiện tại v
        public AdjList(LinkedList<int>[] g)
        {
            v = g;
        }
        // Đọc file AdjList.txt --> danh sách kề v
        public void FileToAdjList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            v = new LinkedList<int>[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = new LinkedList<int>();
                string st = sr.ReadLine();
                // Đặt điều kiện không phải đỉnh cô lập
                if (st != "")
                {
                    string[] s = st.Split();
                    for (int j = 0; j < s.Length; j++)
                    {
                        int x = int.Parse(s[j]);
                        v[i].AddLast(x);
                    }
                }
            }
            sr.Close();
        }
        // Xuất đồ thị
        public void Output()
        {
            Console.WriteLine("Đồ thị danh sách kề - số đỉnh : " + n);
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write("   Đỉnh {0} ->", i);
                foreach (int x in v[i])
                    Console.Write("{0, 3}", x);
                Console.WriteLine();
            }
        }

        bool[] visited;
        int[] pre;
        int[] index;
        int inconnect;
        int[] color;

        public int Inconnect { get => inconnect; set => inconnect = value; }

        public void TraverseDFS(int s)
        {
            //S là đỉnh xuất phát
            visited = new bool[v.Length];
            //Khởi tạo và gán giá trị ban đầu cho visited[]
            //Nếu(chưa đánh dấu đỉnh s)
            if (!visited[s])
            {
                DFS(s);
            }
        }

        public void DFS(int s)
        {
            //Đánh dấu đỉnh s;

            bool[] visited = new bool[v.Length];
            //Duyệt đỉnh s;
            Stack<int> stack = new Stack<int>();
            visited[s] = true;
            stack.Push(s);
            while (stack.Count > 0)
            {
                s = stack.Pop();
                Console.Write(s + "-");
                //Nếu(u chưa được đánh dấu) DFS(u);
                foreach (int u in v[s])
                {
                    if (visited[u])
                        continue;
                    visited[u] = true;
                    stack.Push(u);
                }
            }
        }

        public void DFSRouteXtoY(int x, int y)
        {
            if (IsIsolatedVertex(x) || IsIsolatedVertex(y))
            {
                Console.WriteLine("Không có đường đi");
                return;
            }

            visited = new bool[v.Length];
            pre = new int[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                pre[i] = -1;
            }

            DFS_XtoY(x);

            Console.WriteLine("Đường đi từ {0} đến {1}:", x, y);
            if (pre[y] == -1)
            {
                Console.WriteLine("Không có đường đi tới {0}", y);
            }
            else
            {
                RouteXY(x, y);
            }
        }

        public void DFS_XtoY(int s)
        {
            visited[s] = true;

            foreach (int u in v[s])
            {
                if (!visited[u])
                {
                    pre[u] = s;
                    DFS_XtoY(u);
                }
            }
        }

        public void RouteXY(int x, int y)
        {
            if (x == y)
            {
                Console.Write(y);
            }
            else if (pre[y] == -1)
            {
                Console.WriteLine("Không có đường đi từ {0} đến {1}", x, y);
            }
            else
            {
                RouteXY(x, pre[y]);
                Console.Write(" -> {0}", y);
            }
        }

        public bool IsIsolatedVertex(int vertex)
        {
            return v[vertex].Count == 0;
        }

        public void Connected()
        {
            // inconnect: số TPLT, giá trị ban đầu = 0
            inconnect = 0;

            // index: lưu các đỉnh cùng một TPLT, khởi tạo index[] n phần tử
            index = new int[n];

            // Khởi gán index[i] = -1, i = 0 .. < n
            for (int i = 0; i < n; i++)
                index[i] = -1;

            // Khởi tạo và giá trị ban đầu cho visited[i] = false, Vi = 0 .. < n
            visited = new bool[v.Length];
            for (int i = 0; i < n; i++)
                visited[i] = false;

            // Duyệt từng đỉnh i
            for (int i = 0; i < visited.Length; i++)
            {
                // Nếu chưa duyệt đỉnh i (visited[i] == false)
                if (!visited[i])
                {
                    // Khởi đầu cho một TPLT mới -> tăng inconnect++
                    inconnect++;
                    // Tìm và đánh dấu các đỉnh cùng TPLT, gọi hàm BFS_Connected()
                    DFS_Connected(i);
                }
            }

            //Console.WriteLine("TPLT: " + inconnect);
        }

        public void DFS_Connected(int s)
        {
            // Sử dụng một queue cho giải thuật
            Stack<int> q = new Stack<int>();
            // Duyệt đỉnh s (visited[s] = true)
            visited[s] = true;
            // Đưa s vào q
            q.Push(s);
            // Lặp khi queue q còn phần tử
            while (q.Count > 0)
            {
                // Lấy từ queue q ra một phần tử -> s
                s = q.Pop();
                // gán giá trị TPLT : index[s] = inconnect;
                index[s] = inconnect;
                // Duyệt các đỉnh kề u của s (int u in v[s])
                foreach (int u in v[s])
                {
                    // Nếu u chưa duyệt (visited[u] == false)
                    if (visited[u] == false)
                    {
                        // Duyệt u :
                        visited[u] = true;
                        // Đưa u vào Queue q
                        q.Push(u);
                    }
                }
            }
        }
        public void OutConnected()
        {
            for (int i = 1; i <= inconnect; i++)
            {
                Console.Write("  TPLT {0} : ", i);
                for (int j = 0; j < index.Length; j++)
                    if (index[j] == i)
                        Console.Write(j + " ");
                Console.WriteLine();
            }
        }

        public void RemoveEdgeX(int x)
        {

            for (int i = 0; i < v.Length; i++)
            {
                // Kiểm tra xem cạnh từ đỉnh x đến đỉnh i có tồn tại hay không
                if (i == x)
                    v[i].Clear();
                else
                    v[i].Remove(x);
            }
        }
        public void RemoveEdgeXY(int x, int y)
        {
            // Xóa y trong v[x]
            v[x].Remove(y);
            // Xóa x trong v[y]
            v[y].Remove(x);
        }

        public void Bipartite()
        {
            visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
            }
            // Khởi tạo visited[] và gán giá trị ban đầu : visited[i] = false, i
            // Khởi tạo color[] và gán giá trị ban đầu : color[i] = -1 , i
            color = new int[n];
            for (int i = 0; i < n; i++)
            {
                color[i] = -1;
            }
            // Chọn một đỉnh bất kỳ (giả sử chọn đỉnh 0) và tô màu 1 : color[0] = 1;
            int[] c = new int[n];
            color[0] = 1;
            // Nếu (IsBipartite(0) == true)  "Đồ thị 2 phía"
            if (IsBipartite(0) == true)
                Console.WriteLine("Đồ thị 2 phía");
            // Ngược lại : “Không phải Đồ thị 2 phía"
            else
                Console.WriteLine("Không phải đồ thị 2 phía");
        }

        public bool IsBipartite(int s)
        {
            //Duyệt các đỉnh u kề với s: (int u in v[s])
            foreach (int u in v[s])
            {
                //Nếu u chưa duyệt
                if (visited[u] == false)
                {
                    //Đánh dấu duyệt u
                    visited[u] = true;
                    //Tô màu cho u khác màu với s :
                    color[u] = 1 - color[s];
                    //Nếu(!IsBipartite(u)) // Dừng đệ qui khi gặp lỗi : return false
                    if (!IsBipartite(u))
                        return false;
                }
                //Ngược lại, Nếu 2 đỉnh kề nhau s và u cùng màu :
                else if (color[u] == color[s])
                    return false;
            }
            return true;
        }

        public void DFSTestCycle()
        {
            //Khởi tạo và gán giá trị ban đầu cho color[] : color[i] = 0,i
            color = new int[n];
            for (int i = 0; i < n; i++)
            {
                color[i] = 0; 
            }
            //Duyệt từng đỉnh i(0..n - 1) và nhận phản hồi từ DFSCycle(i) cho mỗi đỉnh
            for (int i = 0; i < n; i++)
            {
                //Nếu DFSCycle(i) = true(-> có chu trình)
                if (DFSCycle(i) == true)
                {
                    Console.WriteLine("Đồ thị có chu trình");
                    //Dừng xét :
                    return;
                }
            }
            Console.WriteLine("Đồ thị không có chu trình");
        }

        public bool DFSCycle(int s)
        {
            //Đang duyệt, tô màu 1 cho đỉnh s:
            this.color[s] = 1;
            //Duyệt tiếp các đỉnh kề của s(int u in v[s])
            foreach (int u in v[s])
            {
                //Nếu chưa duyệt u(u có màu 0) :
                if (color[u] == 0)
                {
                    //gọi đệ qui với u: DFSCycle(u);
                    DFSCycle(u);
                    return true;
                }
                //Ngược lại, nếu u có màu 1 :
                else if (color[u] == 1)
                    //trả về true(có chu trình)
                    return true;
            }
            // Ðến đây đã duyệt xong s -> tô màu 2 :
            this.color[s] = 2;
            return false;
        }

        public void TopologicalSort(int s, LinkedList<int> result)
        {
            this.color[s] = 1;

            foreach (int u in v[s])
            {
                if (this.color[u] == 0)
                {
                    TopologicalSort(u, result);
                }
            }

            this.color[s] = 2;
            result.AddFirst(s);
        }

        public void PrintTopologicalOrder()
        {
            int n = v.Length;
            color = new int[n];
            LinkedList<int> result = new LinkedList<int>();

            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0)
                {
                    TopologicalSort(i, result);
                }
            }

            Console.WriteLine("Thứ tự topo: ");
            foreach (int vertex in result)
            {
                Console.Write(vertex + " ");
            }
            Console.WriteLine();
        }

        public void FileToAdjList1(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            v = new LinkedList<int>[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = new LinkedList<int>();
                string st = sr.ReadLine();
                // Đặt điều kiện không phải đỉnh cô lập
                if (!string.IsNullOrEmpty(st))
                {
                    string[] s = st.Split();
                    for (int j = 0; j < s.Length; j++)
                    {
                        int x = int.Parse(s[j]);
                        v[i].AddLast(x);
                    }
                }
            }
            sr.Close();
        }
    }
}
