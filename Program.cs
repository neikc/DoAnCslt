using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
struct Flashcard
{
    //public string FirstFace { get; set; }:
    //Đây là một thuộc tính (property) của struct Flashcard. Nó có hai phần quan trọng
    //là FirstFace là tên thuộc tính và kiểu dữ liệu là string.
    //Ta có thể đọc và ghi giá trị cho thuộc tính này từ bên ngoài struct.
    //get; set; cho phép bạn đọc và ghi giá trị.
    public string FirstFace { get; set; }
    public string SecondFace { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime NextStudyDate { get; set; }
    public DateTime LastStudiedDate { get; set; }
    public int StudyCount { get; set; }
}

class Program
{
    static string filePath = "flashcards.txt";
    //PHẦN CODE CHÍNH - MENU
    static void Main()
    {
        //sử dụng để đặt bảng mã xuất của bảng mã cho console thành UTF8e.
        //Thiết lập bảng mã xuất để hỗ trợ ký tự UTF8 khi xuất dữ liệu ra console.
        Console.OutputEncoding = Encoding.UTF8;
        Flashcard[] flashcards = LoadFlashcards();

        while (true)
        {
            Console.Clear();
            //----------------------------Menu------------------------------
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╭────────────────────────────────────────────────────╮");
            Console.WriteLine("│                     MENU CHÍNH                     │");
            Console.WriteLine("├────────────────────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│ 1. Quản lý Flashcard                               │");
            Console.WriteLine("│ 2. Ôn tập từ vựng                                  │");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("│ 3. Thoát chương trình                              │");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╰────────────────────────────────────────────────────╯");
            Console.ResetColor();


            //-------------------------------------------------------------
            //Đọc vào lựa chọn
            Console.Write("Lựa chọn của bạn: ");
            string choice = Console.ReadLine();
            //Thực hiện các lựa chọn
            switch (choice)
            {
                //Nếu như chọn phím 1 thì
                case "1":
                    flashcards = Menuquanly(flashcards);
                    break;
                //Nếu như chọn phím 2 thì
                case "2":
                    flashcards = MenuOntap(flashcards);
                    break;
                //Nếu như chọn phím 3 thì
                case "3":
                    SaveFlashcards(flashcards);
                    return;
                //Nếu như chọn các phím ngoài các phím kể trên thì
                default:
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                    Console.WriteLine("Ấn phím Enter để tiếp tục...");
                    Console.ReadLine();
                    break;
            }

        }
    }


    //BỔ TRỢ MENU
    static Flashcard[] MenuOntap(Flashcard[] flashcards)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║            ÔN TẬP TỪ VỰNG            ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║    1. Ôn tập từ vựng theo ngày       ║");
            Console.WriteLine("║    2. Game xáo trộn chữ              ║");
            Console.WriteLine("║    3. Game từ vựng vui               ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║    4. Quay về Menu chính             ║");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("Lựa chọn của bạn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    flashcards = StudyFlashcards(flashcards);
                    break;
                case "2":
                    ArrangingGame(flashcards);
                    break;
                case "3":
                    GameFunny(flashcards);
                    break;
                case "4":
                    return flashcards;
                default:
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                    Console.WriteLine("Ấn phím Enter để tiếp tục...");
                    Console.ReadLine();
                    break;
            }
        }
        return flashcards;
    }

    static Flashcard[] Menuquanly(Flashcard[] flashcards)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║            QUẢN LÝ TỪ VỰNG           ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║   1. Nhập từ mới                     ║");
            Console.WriteLine("║   2. Hiển thị danh sách              ║");
            Console.WriteLine("║   3. Chỉnh sửa                       ║");
            Console.WriteLine("║   4. Tìm kiếm                        ║");
            Console.WriteLine("║   5. Xoá                             ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║   6. Quay lại Menu chính             ║");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("Lựa chọn của bạn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (flashcards.Length < 500000)
                    {
                        //Tạo ra flashcard mới, gán là newflashcard
                        //Create (Từ - Nghĩa, khởi tạo các thông tin
                        //-> Add (Thêm vào trong mảng lưu Flashcard
                        //-> Save (Lưu vào trong file)
                        flashcards = CreateFlashcard(flashcards);
                        LoadFlashcards();
                    }
                    else
                    {
                        Console.WriteLine("Danh sách flashcard đã đầy. Chương trình hiện chỉ lưu tối đa 500.000 flashcard.");
                        Console.WriteLine("Ấn phím Enter để tiếp tục...");
                        Console.ReadLine();
                    }
                    break;
                case "2":
                    DisplayFlashcards(flashcards);
                    break;
                case "3":
                    flashcards = EditFlashcard(flashcards);
                    break;
                case "4":
                    SearchFlashcards(flashcards);
                    break;
                case "5":
                    flashcards = DeleteFlashcards(flashcards);
                    break;
                case "6":
                    return flashcards;
                default:
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                    Console.WriteLine("Ấn phím Enter để tiếp tục...");
                    Console.ReadLine();
                    break;
            }
        }
        return flashcards;
    }



    ////////////////////////////////////////////////////////////////////////
    //PHẦN CODE QUẢN LÝ FLASHCARDS
    //Thủ tục Load các Flashcard đã lưu trong file


    //Thủ tục load Flashcard đã lưu trong file
    static Flashcard[] LoadFlashcards()
    {
        if (File.Exists(filePath))
        {
            //Encoding.UTF8 được thêm vào giúp cho đọc dữ liệu từ file có thể đọc được tiếng Việt
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);


            List<Flashcard> flashcardList = new List<Flashcard>();

            foreach (string line in lines)
            {
                if (line.Length == 0) continue;
                string[] parts = line.Split(',');
                Flashcard flashcard = new Flashcard
                {
                    FirstFace = parts[0],
                    SecondFace = parts[1],
                    LastModified = DateTime.Parse(parts[2]),
                    NextStudyDate = DateTime.Parse(parts[3]),
                    LastStudiedDate = DateTime.Parse(parts[4]),
                    StudyCount = int.Parse(parts[5])
                };
                flashcardList.Add(flashcard);
            }

            return flashcardList.ToArray();
        }
        else
        {
            return new Flashcard[0];
        }
    }

    //Thủ tục lưu Flashcard, cập nhật các thông số
    static void SaveFlashcards(Flashcard[] flashcards)
    {
        // Khởi tạo mảng lines để lưu thông tin của flashcards
        string[] lines = new string[flashcards.Length];

        // Duyệt qua từng flashcard và ánh xạ thành chuỗi
        for (int i = 0; i < flashcards.Length; i++)
        {
            Flashcard flashcard = flashcards[i];
            lines[i] = $"{flashcard.FirstFace},{flashcard.SecondFace},{flashcard.LastModified},{flashcard.NextStudyDate},{flashcard.LastStudiedDate},{flashcard.StudyCount}";
        }

        // Lưu dữ liệu vào file
        //Encoding.UTF8 được thêm vào để có thể ghi vào file dưới dạng tiếng Việt
        File.WriteAllLines(filePath, lines, Encoding.UTF8);

    }


    //Thủ tục tạo Flashcard mới
    static Flashcard[] CreateFlashcard(Flashcard[] flashcards)
    {
        while (true)
        {
            Console.Clear();
            Flashcard flashcard = new Flashcard();
            Console.Write("Nhập từ: ");
            flashcard.FirstFace = Console.ReadLine();
            Console.Write("Nhập nghĩa: ");
            flashcard.SecondFace = Console.ReadLine();
            flashcard.LastModified = DateTime.Now;
            flashcard.NextStudyDate = DateTime.Now.Date;
            flashcard.LastStudiedDate = DateTime.Now.Date;
            flashcard.StudyCount = 0;
            flashcards = AddFlashcard(flashcards, flashcard);

            Console.WriteLine("Flashcard đã được thêm!");
            Console.Write("Nhập X để thoát, bấm Enter để tiếp tục...");
            string response = Console.ReadLine().Trim().ToUpper();

            if (response == "X")
            {
                break;
            }
        }
        return flashcards;
    }

    // Thủ tục Thêm flashcard
    static Flashcard[] AddFlashcard(Flashcard[] flashcards, Flashcard newFlashcard)
    {
        // Sử dụng phương thức Append để thêm một phần tử vào mảng flashcards.
        // Lệnh này trả về một chuỗi mới chứa tất cả các phần tử trong mảng ban đầu cùng với phần tử mới được thêm vào.
        // Mảng ban đầu không bị thay đổi, và chúng ta chuyển đổi kết quả trả về thành một mảng để thay thế mảng ban đầu.
        return flashcards.Append(newFlashcard).ToArray();
    }


    //Thủ tục hiển thị các flashcard
    static void DisplayFlashcards(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào trong kho từ");
            Console.WriteLine("Ấn phím Enter để tiếp tục...");
            Console.ReadLine();
            return;
        }
        Console.WriteLine($"Danh sách các flashcard ({flashcards.Length} từ):");
        for (int i = 0; i < flashcards.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{i + 1}. Từ: {flashcards[i].FirstFace} --- Nghĩa: {flashcards[i].SecondFace}");

            //ToShortDateString giúp chỉ hiển thị ngày, không hiển thị giờ
            Console.WriteLine($"     Học tiếp vào ngày {flashcards[i].NextStudyDate.ToString("dd/MM/yyyy")}");

            Console.ResetColor();
        }
        Console.WriteLine("Ấn phím Enter để tiếp tục...");
        Console.ReadLine();
    }

    // Thủ tục chỉnh sửa flashcard
    static Flashcard[] EditFlashcard(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào để chỉnh sửa");
            Console.WriteLine("Ấn phím Enter để tiếp tục...");
            Console.ReadLine();
            return flashcards;
        }
        bool continueEdit = true;

        while (continueEdit)
        {
            //In ra danh sách các flashcard
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Danh sách các flashcard:");
            Console.WriteLine("0. Thoát chính sửa");
            for (int i = 0; i < flashcards.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{i + 1}. Từ: {flashcards[i].FirstFace}");
                Console.WriteLine($"   Nghĩa: {flashcards[i].SecondFace}");
                Console.ResetColor();
            }

            Console.Write("Chọn flashcard để chỉnh sửa (nhập số thứ tự): ");
            int index = -1;
            bool isValidIndex = false;

            while (!isValidIndex)
            {
                Console.Write("Lựa chọn của bạn: ");
                string input = Console.ReadLine();

                //Sử dụng try-catch để kiểm soát lỗi ở đây
                //try-catch là một cấu trúc trong lập trình được sử dụng để
                //xử lý ngoại lệ (exceptions) - những điều kiện không bình thường hoặc lỗi trong quá trình thực thi chương trình.
                //Cấu trúc này cho phép bạn kiểm soát và xử lý các tình huống ngoại lệ một cách chủ động,
                //thay vì để chương trình bị "crash" mà không có sự can thiệp từ phía người lập trình.

                //Trong đoạn mã trên, nếu có lỗi khi chuyển đổi input thành số nguyên (FormatException),
                //nó sẽ nhảy vào khối catch và hiển thị một thông báo lỗi thân thiện
                //với người dùng thay vì chương trình bị lỗi.
                try
                {
                    int parsedIndex = int.Parse(input); // Thử chuyển đổi chuỗi thành số nguyên
                    //Nếu người ta bấm 0 là người ta không muốn chỉnh nữa
                    if (parsedIndex == 0) return flashcards;

                    parsedIndex--; // Chuyển đổi thành chỉ số mảng bằng cách giảm đi 1, do mảng bắt đầu từ 0

                    // Nếu STT của flashcard đó nằm trong khoảng khả thi của mảng lưu flashcard thì mới làm để tránh lỗi
                    if (parsedIndex >= 0 && parsedIndex < flashcards.Length)
                    {
                        index = parsedIndex;
                        isValidIndex = true;
                    }
                    else
                    {
                        Console.WriteLine("Số thứ tự không hợp lệ. Vui lòng nhập lại.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Số thứ tự không hợp lệ. Vui lòng nhập lại.");
                }
            }

            //Lưu lại flashcard đã chọn stt vào biến selectedFlashcard
            Flashcard selectedFlashcard = flashcards[index];
            string editChoice;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("           CHỌN MỘT TÙY CHỌN          ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"1. Chỉnh sửa từ ({selectedFlashcard.FirstFace})");
                Console.WriteLine($"2. Chỉnh sửa nghĩa ({selectedFlashcard.SecondFace})");
                Console.WriteLine($"3. Chỉnh sửa cả từ và nghĩa");
                Console.WriteLine($"4. Thoát chỉnh sửa");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.ResetColor();

                Console.Write("Lựa chọn của bạn: ");
                editChoice = Console.ReadLine();

                switch (editChoice)
                {
                    case "1":
                        Console.Write("Nhập từ mới: ");
                        selectedFlashcard.FirstFace = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Nhập nghĩa mới: ");
                        selectedFlashcard.SecondFace = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Nhập từ mới: ");
                        selectedFlashcard.FirstFace = Console.ReadLine();
                        Console.Write("Nhập nghĩa mới: ");
                        selectedFlashcard.SecondFace = Console.ReadLine();
                        break;
                    case "4":
                        //Nếu chọn 4 là không muốn chỉnh sửa nữa
                        return flashcards;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                        Console.WriteLine("Ấn phím Enter để tiếp tục...");
                        Console.ReadLine();
                        break;
                }
            } while (editChoice != "1" && editChoice != "2");

            // Cập nhật ngày chỉnh sửa
            selectedFlashcard.LastModified = DateTime.Now;
            flashcards[index] = selectedFlashcard;
            Console.WriteLine("Flashcard đã được điều chỉnh!");
            Console.Write("Nhập Y để tiếp tục chỉnh sửa, nếu không Enter để tiếp tục... ");
            string response = Console.ReadLine().Trim().ToUpper();

            if (response != "Y")
            {
                continueEdit = false;
            }
        }

        return flashcards;
    }


    // Thủ tục tìm kiếm flashcard
    static void SearchFlashcards(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào để tìm kiếm");
            Console.WriteLine("Ấn phím Enter để tiếp tục...");
            Console.ReadLine();
            return;
        }
        Console.Write("Nhập từ cần tra trong kho từ: ");
        string keyword = Console.ReadLine().ToLower();

        List<Flashcard> results = new List<Flashcard>();

        foreach (Flashcard flashcard in flashcards)
        {
            if (flashcard.FirstFace.ToLower() == keyword) results.Add(flashcard);
        }

        if (results.Count == 0)
        {
            Console.WriteLine("Không tìm thấy kết quả nào.");
            Console.WriteLine("Ấn phím Enter để tiếp tục...");
            Console.ReadLine();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"🔍 Kết quả tìm kiếm cho '{keyword}':");
            Console.ResetColor();

            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"    📚 {i + 1}. {flashcards[i].FirstFace} --- {flashcards[i].SecondFace}");
                Console.WriteLine($"       📅 Ngày học tiếp theo: {results[i].NextStudyDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"       📅 Ngày học gần nhất: {results[i].LastStudiedDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"       📅 Ngày chỉnh sửa: {results[i].LastModified.ToString("dd/MM/yyyy HH:mm:ss")}");
                Console.WriteLine("Bấm phím Enter để tiếp tục");
                Console.ReadLine();
            }

        }
    }


    //Thủ tục xóa flashcard
    static Flashcard[] DeleteFlashcards(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào để xóa");
            Console.WriteLine("Bấm phím Enter để tiếp tục");
            Console.ReadLine();
            return flashcards;
        }
        bool continueDeleting = true;

        while (continueDeleting)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Danh sách các flashcard:");
            //Nếu chọn số 0 thì tức là người ta không muốn xóa nữa
            Console.WriteLine("0. Thoát xóa");

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < flashcards.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {flashcards[i].FirstFace} --- {flashcards[i].SecondFace}");
            }
            Console.ResetColor();

            Console.Write("Chọn flashcard để xóa (nhập số thứ tự): ");

            Console.Write("Chọn flashcard để chỉnh sửa (nhập số thứ tự): ");
            int index = -1;
            bool isValidIndex = false;

            while (!isValidIndex)
            {
                Console.Write("Lựa chọn của bạn: ");
                string input = Console.ReadLine();

                try
                {
                    int parsedIndex = int.Parse(input);
                    
                    //Nếu mà nhập vào số 0 thì thoát do người ta không muốn xóa nữa
                    if (parsedIndex ==0 ) return flashcards;

                    parsedIndex--;

                    //Làm tiếp, nếu mà người ta nhập đúng thì mới cho qua, không thì phải nhập lại
                    if (parsedIndex >= 0 && parsedIndex < flashcards.Length)
                    {
                        index = parsedIndex;
                        isValidIndex = true;
                    }
                    else
                    {
                        Console.WriteLine("Số thứ tự không hợp lệ. Vui lòng nhập lại.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Số thứ tự không hợp lệ. Vui lòng nhập lại.");
                }
            }

            // Tạo một danh sách mới không bao gồm flashcard cần xóa
            List<Flashcard> updatedFlashcardList = flashcards.ToList();

            //updatedFlashcardList.RemoveAt(index - 1);
            //được sử dụng để xóa một phần tử cụ thể từ List<Flashcard> có tên là updatedFlashcardList.

            //RemoveAt(index - 1): Phương thức này xóa phần tử tại chỉ số được chỉ định từ List.
            //Trong trường hợp này, index - 1 là chỉ số của phần tử cần xóa.
            //Do mảng và List bắt đầu từ chỉ số 0, nên index - 1 được sử dụng để chuyển đổi
            //giữa chỉ số của người dùng (bắt đầu từ 1) và chỉ số của List (bắt đầu từ 0).
            updatedFlashcardList.RemoveAt(index - 1);
            flashcards = updatedFlashcardList.ToArray();
            Console.WriteLine("Flashcard đã được xóa!");
            

            Console.Write("Nhập Y để tiếp tục xóa, nếu không bấm phím Enter để tiếp tục...");
            string response = Console.ReadLine().Trim().ToUpper();

            if (response != "Y")
            {
                continueDeleting = false;
            }
        }

        return flashcards;
    }

    ////////////////////////////////////////////////////////////////////////
    //PHẦN CODE ÔN TẬP
    //Thủ tục học Flashcard thông thường
    static Flashcard[] StudyFlashcards(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào để học, hãy nhập thêm từ vựng");
            Console.WriteLine("Bấm phím Enter để tiếp tục...");
            Console.ReadLine();
            return flashcards;
        }

        // Dãy số Fibonacci
        int[] fibonacciNumbers = GenerateFibonacci(30);

        //DateTime today = ...;: Là cách khai báo và khởi tạo biến today kiểu DateTime,
        //được gán giá trị là thời điểm là ngày hôm nay.
        DateTime today = DateTime.Now.Date;
        bool isStudyToday = false;

        //Chạy đến khi nào không còn từ hôm nay học nữa
        while (true)
        {
            for (int i = 0; i < flashcards.Length; i++)
            {
                Console.Clear();
                Console.WriteLine($"Từ vựng {today} cần ôn tập");
                Console.WriteLine("Hướng dẫn: Bấm Enter để xem nghĩa của từ");
                Console.WriteLine();

                if (flashcards[i].NextStudyDate <= today)
                {
                    isStudyToday = true;
                    Console.WriteLine($"     Từ vựng: {flashcards[i].FirstFace}");
                    Console.ReadLine();
                    Console.WriteLine($"     Nghĩa: {flashcards[i].SecondFace}");
                    Console.Write("     Nhớ (Bấm 1), Quên (Bấm 2): ");
                    string RememberStatus = Console.ReadLine();

                    switch (RememberStatus)
                    {
                        case "1":
                            int fibonacciIndex = flashcards[i].StudyCount;

                            // Cập nhật NextStudyDate dựa trên chuỗi Fibonacci
                            //TimeSpan.FromDays là một phương thức tạo mới của lớp TimeSpan trong C#,
                            //được sử dụng để tạo một đối tượng TimeSpan từ một số ngày cụ thể.

                            //TimeSpan là một class được sử dụng để biểu diễn một khoảng thời gian
                            TimeSpan fibonacciInterval = TimeSpan.FromDays(fibonacciNumbers[fibonacciIndex]);
                            flashcards[i].NextStudyDate = today + fibonacciInterval;

                            flashcards[i].LastStudiedDate = today;
                            flashcards[i].StudyCount++;
                            break;
                        case "2":
                            flashcards[i].NextStudyDate = today;
                            flashcards[i].LastStudiedDate = today;
                            if (flashcards[i].StudyCount > 0) flashcards[i].StudyCount--;
                            break;
                        default:
                            Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                            Console.WriteLine("Bấm phím Enter để tiếp tục...");
                            Console.ReadLine();
                            break;
                    }

                    Console.WriteLine("   Bấm X để thoát, bấm phím Enter để tiếp tục...");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.X)
                    {
                        return flashcards;
                    }
                }
            }

            if (isStudyToday == false)
            {
                //Khi không còn từ để học hôm nay nữa thì không lặp nữa
                Console.WriteLine("Hôm nay không có từ vựng nào cần ôn");
                Console.WriteLine("Bấm phím Enter để tiếp tục...");
                Console.ReadLine();
                break;
            }
        }

        return flashcards;
    }

    // Hàm sinh dãy số Fibonacci sử dụng System.Math
    static int[] GenerateFibonacci(int n)
    {
        //Sử dụng công thức Binet (Binet Fomular để tính tổng dãy số Fibonacci)
        //Công thức cho biết Tổng của dãy số fibonaci từ số thứ 1 đến số thứ x,
        //trong cống thức x chính là giá trị của số fibonaci thứ x.
        int[] fibonacciNumbers = new int[n];
        for (int i = 0; i < n; i++)
        {
            fibonacciNumbers[i] = (int)Math.Round((Math.Pow((1 + Math.Sqrt(5)) / 2, i + 1) - Math.Pow((1 - Math.Sqrt(5)) / 2, i + 1)) / Math.Sqrt(5));
        }
        return fibonacciNumbers;
    }


    //Thủ tục Game Sắp xếp lại thứ tự chữ cái trong từ
    static void ArrangingGame(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào để chơi");
            Console.WriteLine("Bấm phím Enter để tiếp tục...");
            Console.ReadLine();
            return;
        }

        //Tối thiểu 5 từ để bắt đầu trò chơi
        if (flashcards.Length < 5)
        {
            Console.WriteLine("Bạn cần ít nhất 5 từ vựng trong kho từ để bắt đầu trò chơi, hãy quay lại sau");
            Console.WriteLine("Bấm phím Enter để quay về menu...");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("Chào mừng đến với trò chơi sắp xếp từ!");
        Console.WriteLine("Mỗi lượt chơi, bạn sẽ được ôn lại 5 từ vựng");

        int totalWords = flashcards.Length;
        int correctCount = 0;
        int incorrectCount = 0;
        int wordIndex = 0;

        
        
        //new Random(): Là cách khởi tạo một đối tượng của lớp Random.
        //Phương thức khởi tạo này không yêu cầu bất kỳ đối số nào,
        //điều này có nghĩa là chúng ta đang sử dụng một hàm tạo mặc định của lớp Random.
        Random random = new Random();


        //Trộn mảng ngẫu nhiên, mảng wordIndicies chứa các số ngẫu nhiên không theo thứ tự từ 0 đến số từ có trong kho từ vựng
        //Tạo một mảng chứa chỉ số của từng từ (ngẫu nhiên) để khi chơi không có chuyện chơi lại câu đố cũ
        //Do nếu chỉ random thông thường có thể sẽ random trùng lại giá trị cũ
        //Enumerable.Range được sử dụng để tạo ra một dãy số từ 0 đến totalWords - 1.
        //và sau đó chúng ta gọi ToArray() để chuyển nó thành một mảng các số nguyên (int[]).
        int[] wordIndices = Enumerable.Range(0, totalWords).ToArray();

        // Trộn mảng chỉ số để chọn từ ngẫu nhiên
        //Đoạn mã là một phần của thuật toán Fisher-Yates Shuffle (hoặc còn được gọi là Knuth Shuffle)
        //một thuật toán được sử dụng để trộn (hoặc làm ngẫu nhiên hoán vị) các phần tử trong một mảng.
        for (int i = 0; i < totalWords - 1; i++)
        {
            //random.Next(min, max);: Sinh một số nguyên ngẫu nhiên trong khoảng từ min đến max(không bao gồm max).
            int j = random.Next(i, totalWords);

            int temp = wordIndices[i];
            wordIndices[i] = wordIndices[j];
            wordIndices[j] = temp;
        }

        //Đoạn bắt đầu trò chơi
        for (wordIndex=0; wordIndex < wordIndices.Length; wordIndex++)
        {
            //Câu đố hiện tại là currentIndex
            int currentIndex = wordIndices[wordIndex];
            Console.Clear();

            string word = flashcards[currentIndex].FirstFace;
            string meaning = flashcards[currentIndex].SecondFace;

            //Xáo trộn các kí tự trong từ đang đố
            char[] scrambledWord = ShuffleWord(word, random);

            Console.WriteLine("     Từ đã bị xáo trộn: " + new string(scrambledWord));

            string userInput;
            while (true)
            {
                Console.Write("     Hãy nhập từ đoán: ");
                userInput = Console.ReadLine();
                //Nếu trả lời đúng
                if (userInput == word)
                {
                    Console.WriteLine("     Chính xác!");
                    Console.WriteLine($"     Từ và nghĩa: {word} --- {meaning}");
                    correctCount++;
                    incorrectCount = 0;
                    Console.WriteLine("Bấm phím Enter để tiếp tục...");
                    Console.ReadLine();
                    break;
                }
                //Nếu trả lời sai
                else
                {
                    incorrectCount++;
                    //Nếu sai ba lần, chưa đến 3 lần
                    if (incorrectCount < 3)
                    {
                        Console.WriteLine("     Sai! Bạn còn " + (3 - incorrectCount) + " lượt thử.");
                        Console.WriteLine("Bấm phím Enter để tiếp tục...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("     Bạn đã nhập sai quá 3 lần");
                        Console.WriteLine($"     Đáp án: {word} ({meaning})");
                        incorrectCount = 0;
                        Console.WriteLine("Bấm phím Enter để tiếp tục...");
                        Console.ReadLine();
                        break;
                    }
                }
            }

            //Chơi được 5 từ sẽ hỏi có muốn chơi tiếp nữa không
            if (wordIndex % 5 == 0 && wordIndex < totalWords)
            {
                while (true)
                {
                    try
                    {
                        Console.Write("     Bạn có muốn tiếp tục chơi không? (1: Yes, 2: No): ");
                        string continueInput = Console.ReadLine();

                        // Kiểm tra điều kiện để thoát khỏi vòng lặp
                        if (continueInput == "2")
                        {
                            Console.WriteLine("     === Kết Quả Trò Chơi ===");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("     Bạn đã hoàn thành trò chơi!");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"        Số từ đúng: {correctCount}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"        Số từ sai: {incorrectCount}");
                            Console.ResetColor();

                            Console.ReadLine();
                            return;
                        }
                        else if (continueInput == "1")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Vui lòng nhập lại.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                        Console.WriteLine("Vui lòng nhập lại.");
                    }
                }
            }
        }

        Console.WriteLine("     === Kết Quả Trò Chơi ===");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("     Bạn đã hoàn thành trò chơi!");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"        Số từ đúng: {correctCount}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"        Số từ sai: {incorrectCount}");
        Console.ResetColor();

        Console.WriteLine("Bấm phím Enter để tiếp tục...");
        Console.ReadLine();
    }


    static char[] ShuffleWord(string word, Random random)
    {
        //Đoạn mã là một phần của thuật toán Fisher-Yates Shuffle (hoặc còn được gọi là Knuth Shuffle)
        //một thuật toán được sử dụng để trộn (hoặc làm ngẫu nhiên hoán vị) các phần tử trong một mảng.
        char[] characters = word.ToCharArray();
        int length = characters.Length;
        while (length > 1)
        {
            int index = random.Next(length--);
            //Đổi chỗ vị trí length và index
            char temp = characters[length];
            characters[length] = characters[index];
            characters[index] = temp;
        }
        return characters;
    }

    //-----------------------------------------------------------------------------------------
    static void RenderGraphic(int num)
    {
        string[] frames =
{
	#region Frames
	// 0
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	// 1
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	// 2
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	// 3
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	// 4
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	// 5
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"       \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	// 6
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	#endregion
};
        //Console.CursorLeft: Đây là thuộc tính của lớp Console và đại diện cho vị trí hiện tại của con trỏ theo chiều ngang
        //(từ bên trái của màn hình). Giá trị này được đo lường bằng số ký tự.
        int x = Console.CursorLeft;
        
        //Console.CursorTop: Là thuộc tính của lớp Console và đại diện cho vị trí hiện tại của con trỏ theo chiều dọc
        //(từ trên cùng của màn hình). Giá trị này được đo lường bằng số dòng.
        int y = Console.CursorTop;

        for (int i = 0; i < frames.Length; i++)
        {
            if (i == num)
            {
                string frame = frames[i];

                foreach (char c in frame)
                {
                    if (c == '\n')
                    {
                        Console.WriteLine();

                        //Console.SetCursorPosition(x, ++y);
                        //được sử dụng để di chuyển con trỏ đến một vị trí mới (tọa độ x, y) trên màn hình console.
                        Console.SetCursorPosition(x, ++y);
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }

                //Sau khi tìm được cái để in rồi thì thoát ra không lặp nữa
                break;
            }
        }
    }

    static void DeathGraphic()
    {
        string[] frames =
{
	#region Frames
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o>  ║   " + '\n' +
    @"     /|   ║   " + '\n' +
    @"      >\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o>  ║   " + '\n' +
    @"     /|   ║   " + '\n' +
    @"      >\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o>  ║   " + '\n' +
    @"     /|   ║   " + '\n' +
    @"      >\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      /   ║   " + '\n' +
    @"      \   ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    |__   ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    \__   ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"   ____   ║   " + '\n' +
    @"    ══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"    __    ║   " + '\n' +
    @"   /══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    _ '   ║   " + '\n' +
    @"  _/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @" __/══════╩═══",
	//
	@"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",
	#endregion
};

        foreach (var frame in frames)
        {
            Console.Clear();
            Console.WriteLine(frame);
            Thread.Sleep(100);
        }
    }
    //-----------------------------------------------------------------------------------------

    static void GameFunny(Flashcard[] flashcards)
    {
        if (flashcards.Length == 0)
        {
            Console.WriteLine("Không có từ vựng nào để ôn tập");
            Console.WriteLine("Bấm phím Enter để tiếp tục...");
            Console.ReadLine();
            return;
        }
        Console.Clear();
        Console.WriteLine("    Trò chơi đoán từ");
        Console.WriteLine();

        // Lấy ngẫu nhiên một flashcard từ danh sách
        int totalWords = flashcards.Length;
        int wordIndex = 0;
        Random random = new Random();

        //XỬ LÝ GIỐNG Ở GAME TRÊN
        //Trộn mảng ngẫu nhiên, mảng wordIndicies chứa các số ngẫu nhiên không theo thứ tự từ 0 đến số từ có trong kho từ vựng
        //Tạo một mảng chứa chỉ số của từng từ (ngẫu nhiên) để khi chơi không có chuyện chơi lại câu đố cũ
        //Do nếu chỉ random thông thường có thể sẽ random trùng lại giá trị cũ
        //Enumerable.Range được sử dụng để tạo ra một dãy số từ 0 đến totalWords - 1.
        //và sau đó chúng ta gọi ToArray() để chuyển nó thành một mảng các số nguyên (int[]).
        int[] wordIndices = Enumerable.Range(0, totalWords).ToArray();

        // Trộn mảng chỉ số để chọn từ ngẫu nhiên
        //Đoạn mã là một phần của thuật toán Fisher-Yates Shuffle (hoặc còn được gọi là Knuth Shuffle)
        //một thuật toán được sử dụng để trộn (hoặc làm ngẫu nhiên hoán vị) các phần tử trong một mảng.
        //Trong trường hợp này, mảng được trộn là wordIndices.
        for (int i = 0; i < totalWords - 1; i++)
        {
            int j = random.Next(i, totalWords);
            int temp = wordIndices[i];
            wordIndices[i] = wordIndices[j];
            wordIndices[j] = temp;
        }


        for (wordIndex = 0; wordIndex < totalWords; ++wordIndex)
        {
            int currentIndex = wordIndices[wordIndex];
            Flashcard randomFlashcard = flashcards[currentIndex];

            // Lấy từ từ flashcard
            string randomWord = randomFlashcard.FirstFace.ToLower();

            // Hiển thị gợi ý cho người chơi
            Console.WriteLine($"Gợi ý: {randomFlashcard.SecondFace}");

            char[] revealedChars = new string('_', randomWord.Length).ToCharArray();
            int incorrectGuesses = 0;

            while (incorrectGuesses < randomWord.Length && revealedChars.Contains('_'))
            {
                RenderGameState(incorrectGuesses, revealedChars);
                Console.WriteLine($"    Gợi ý nghĩa: {randomFlashcard.SecondFace}");

                //Lấy ký tự người chơi đoán
                Console.Write("    Đoán một ký tự: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                char guess = keyInfo.KeyChar;


                bool correctGuess = false;
                for (int i = 0; i < revealedChars.Length; i++)
                {
                    if (revealedChars[i] == '_' && randomWord[i] == guess)
                    {
                        revealedChars[i] = guess;
                        correctGuess = true;
                    }
                }

                if (correctGuess==false)
                    incorrectGuesses++;
                //Nếu như sai quá 7 lần thì thua luôn
                if (incorrectGuesses >= 7) break;
            }

            // Hiển thị kết quả cuối cùng
            if (incorrectGuesses >= 7 || incorrectGuesses>=randomFlashcard.FirstFace.Length)
            {
                DeathGraphic();
                Console.WriteLine("    Bạn đã thua cuộc!");
                Console.WriteLine($"    Đáp án: {randomFlashcard.FirstFace} --- {randomFlashcard.SecondFace}");
                Console.WriteLine("    Bấm phím Enter để tiếp tục...");
                Console.ReadLine();
            }
            else
            {
                RenderGameState(incorrectGuesses, revealedChars);
                Console.WriteLine("    Bạn chiến thắng!");
                Console.WriteLine("    Bấm phím Enter để tiếp tục...");
                Console.ReadLine();
            }

            //GIỐNG Ở GAME TRÊN, luyện 5 từ sẽ được hỏi là có muốn chơi tiếp hay không
            if (wordIndex % 5 == 0)
            {
                while (true)
                {
                    try
                    {
                        Console.Write("Bạn có muốn tiếp tục chơi không? (1: Yes, 2: No): ");
                        string continueInput = Console.ReadLine();

                        // Kiểm tra điều kiện để thoát khỏi vòng lặp
                        if (continueInput == "2")
                        {
                            return;
                        }
                        else if (continueInput == "1")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Vui lòng nhập lại.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                        Console.WriteLine("Vui lòng nhập lại.");
                    }
                }
            }
        }
    }

    // Hàm hiển thị trạng thái của trò chơi
    static void RenderGameState(int incorrectGuesses, char[] revealedChars)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("    Trò chơi đoán từ");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"    Số lần đoán sai: {incorrectGuesses}");
        RenderGraphic(incorrectGuesses);
        Console.ResetColor();
        Console.Write("    Từ cần đoán: ");
        foreach (char c in revealedChars)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }

}
