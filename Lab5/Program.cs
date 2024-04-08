using Lab5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program
    {

        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new();
            string title = "TÌM KIẾM TRÊN ĐỒ THỊ BẰNG THUẬT TOÁN BFS (Breadth First Search)"; string[] ms = { 
                "1. Bài 1 : Tìm các đỉnh liên thông với x",
                "2. Bài 2 : Tìm đường đi từ đỉnh x -> y",
                "3. Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT",
                "5. Baif 5: Đồ thị có chu trình hay không?",
                "0. Thoát" };
            int chon;
            do
            {
                Console.Clear();
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {   // Bài 1
                            // Tạo đường dẫn file filePath = "../../../TextFile/AdjList.txt";
                            string fileInput = "../../../TextFile/AdjList.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput);
                            g.Output();
                            Console.Write("  Nhập đỉnh xuất phát s : ");
                            int s = int.Parse(Console.ReadLine());
                            Console.Write("  Các đỉnh liên thông với {0} : ", s);
                            // Gọi phương thức DFS(s);
                            g.TraverseDFS(s);
                            break;
                        }
                    case 2:
                        {   // Bài 2 : Tìm đường đi từ đỉnh x -> y
                            // Tạo đường dẫn file filePath = "../../../TextFile/AdjList2.txt";
                            string fileInput = "../../../TextFile/AdjList.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput);
                            g.Output();
                            Console.Write("  Nhập đỉnh xuất phát x : ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("        Nhập đỉnh đến y : ");
                            int y = int.Parse(Console.ReadLine());
                            // Gọi phương thức BFS_XtoY(x, y);
                            g.DFSRouteXtoY(x, y);
                            break;
                        }
                    case 3:
                        {   // Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT
                            // Tạo đường dẫn file filePath = "../../../TextFile/AdjList2.txt";
                            string fileInput = "../../../TextFile/AdjList.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput);
                            g.Output();
                            g.Connected();
                            if (g.Inconnect == 1)
                                Console.WriteLine("  Đồ thị liên thông");
                            else
                            {
                                Console.WriteLine("  Đồ thị có {0} thành phần liên thông", g.Inconnect);
                                g.OutConnected();    // Xuất các TPLT
                            }
                            break;
                        }
                    case 4:
                        {
                            string fileInput = "../../../TextFile/Biparite.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput);
                            g.Output();
                            g.Bipartite();
                            break;
                        }
                    case 5:
                        {
                            string fileInput = "../../../TextFile/Cycle.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput);
                            g.Output();
                            g.DFSTestCycle();
                            break;
                        }
                    case 6:
                        {
                            string fileInput = "../../../TextFile/Cycle.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput);
                            g.Output();
                            g.PrintTopologicalOrder();
                            break;
                        }
                    case 7:
                        {
                            string fileInput = "../../../TextFile/Xaynha.txt";
                            // Khởi tạo đồ thị g :
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList1(fileInput);
                            g.Output();
                            g.DFSTestCycle();
                            g.PrintTopologicalOrder();
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}