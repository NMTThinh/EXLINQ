using LINQ;
Console.OutputEncoding = System.Text.Encoding.UTF8;
// Create a data source by using a collection initializer.
IEnumerable<Student> students =
[
    new Student(First: "Svetlana", Last: "Omelchenko", ID: 111, Scores: [97, 92, 81, 60]),
    new Student(First: "Claire",   Last: "O'Donnell",  ID: 112, Scores: [75, 84, 91, 39]),
    new Student(First: "Sven",     Last: "Mortensen",  ID: 113, Scores: [88, 94, 65, 91]),
    new Student(First: "Cesar",    Last: "Garcia",     ID: 114, Scores: [97, 89, 85, 82]),
    new Student(First: "Debra",    Last: "Garcia",     ID: 115, Scores: [35, 72, 91, 70]),
    new Student(First: "Fadi",     Last: "Fakhouri",   ID: 116, Scores: [99, 86, 90, 94]),
    new Student(First: "Hanying",  Last: "Feng",       ID: 117, Scores: [93, 92, 80, 87]),
    new Student(First: "Hugo",     Last: "Garcia",     ID: 118, Scores: [92, 90, 83, 78]),

    new Student("Lance",   "Tucker",      119, [68, 79, 88, 92]),
    new Student("Terry",   "Adams",       120, [99, 82, 81, 79]),
    new Student("Eugene",  "Zabokritski", 121, [96, 85, 91, 60]),
    new Student("Michael", "Tucker",      122, [94, 92, 91, 91])
];
//Học sinh có điểm đầu tiên >90đ
IEnumerable<Student> studentQuery1 =
    from student in students
    where student.Scores[0] > 90
    select student;
//Học sinh có điểm đầu tiên >90đ và điểm cuối < 80đ
IEnumerable<Student> studentQuery2 =
    from student in students
    where student.Scores[0] > 90 && student.Scores[3] < 80
    select student;
//Học sinh có điểm đầu tiên >90đ và sắp xếp theo thứ tự bảng chữ cái
IEnumerable<Student> studentQuery3 =
                    from student in students
                    where student.Scores[0] > 90
                    orderby student.Last ascending
                    select student;
//Học sinh có điểm đầu tiên >90đ sắp xếp từ cao đến thấp
IEnumerable<Student> studentQuery4 =
    from student in students
    where student.Scores[0] > 90
    orderby student.Last ascending
    select student;
//Nhóm học sinh có cùng chữ cái đầu 
IEnumerable<IGrouping<char, Student>> studentQuery5 =
    from student in students
    group student by student.Last[0];
//Nhóm học sinh và sắp xếp theo bảng chữ cái
var studentQuery6 =
                from student in students
                group student by student.Last[0] into studentGroup
                orderby studentGroup.Key
                select studentGroup;
//Điểm trung bình của cả lớp
var studentQuery =
               from student in students
               let totalScore = student.Scores[0] + student.Scores[1] +
                   student.Scores[2] + student.Scores[3]
               select totalScore;

double averageScore = studentQuery.Average();
//Những bạn có họ là Garcia
IEnumerable<string> studentQuery7 =
    from student in students
    where student.Last == "Garcia"
    select student.First;
//Những học sinh có điểm trung bình lớn hơn điểm trung bình lớp
var aboveAverageQuery =
   from student in students
   let x = student.Scores[0] + student.Scores[1] +
       student.Scores[2] + student.Scores[3]
   where x > averageScore
   select new { id = student.ID, score = x };
//MENU
Console.WriteLine("Nhập 1:Học sinh có điểm đầu tiên >90đ ");
Console.WriteLine("Nhập 2:Học sinh có điểm đầu tiên >90đ và điểm cuối < 80đ ");
Console.WriteLine("Nhập 3:Học sinh có điểm đầu tiên >90đ và sắp xếp theo thứ tự bảng chữ cái ");
Console.WriteLine("Nhập 4:Học sinh có điểm đầu tiên >90đ sắp xếp từ cao đến thấp ");
Console.WriteLine("Nhập 5:Nhóm học sinh có cùng chữ cái đầu ");
Console.WriteLine("Nhập 6:Nhóm học sinh và sắp xếp theo bảng chữ cái ");
Console.WriteLine("Nhập 7:Điểm trung bình của cả lớp ");
Console.WriteLine("Nhập 8:Những bạn có họ là Garcia ");
Console.WriteLine("Nhập 9:Những học sinh có điểm trung bình lớn hơn điểm trung bình lớp ");
Console.Write("Nhập số cần bạn cần: ");
    string input = Console.ReadLine();
    if(int.TryParse(input,out int value))
    {
        switch (value)
        {
            case 1:
                Console.WriteLine("Học sinh có điểm đầu tiên >90đ");
                foreach (Student student in studentQuery1)
                {
                    Console.WriteLine($"{student.Last}, {student.First}");
                }
                break;
            case 2:
                Console.WriteLine("Học sinh có điểm đầu tiên >90đ và điểm cuối < 80đ");
                foreach (Student student in studentQuery2)
                {
                    Console.WriteLine($"{student.Last}, {student.First}");
                }
                break;
            case 3:Console.WriteLine("Học sinh có điểm đầu tiên >90đ và sắp xếp theo thứ tự bảng chữ cái");
                foreach (Student student in studentQuery3)
                {
                    Console.WriteLine($"{student.Last}, {student.First}");
                }
                break;
            case 4:
                Console.WriteLine("Học sinh có điểm đầu tiên >90đ sắp xếp từ cao đến thấp");
                foreach (Student student in studentQuery4)
                {
                    Console.WriteLine($"{student.Last}, {student.First} {student.Scores[0]}");
                }
                break;
            case 5:Console.WriteLine("Nhóm học sinh có cùng chữ cái đầu");
            foreach (IGrouping<char, Student> studentGroup in studentQuery5)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (Student student in studentGroup)
                {
                    Console.WriteLine($"   {student.Last}, {student.First}");
                }
            }
            break;
            case 6: Console.WriteLine("Nhóm học sinh và sắp xếp theo bảng chữ cái");
            
            foreach (var groupOfStudents in studentQuery6)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine($"   {student.Last}, {student.First}");
                }
            }
            break;
            case 7:Console.WriteLine("Điểm trung bình của cả lớp");
            Console.WriteLine("Class average score = {0}", averageScore);
            break;
            case 8:
            Console.WriteLine("Những bạn có họ là Garcia");
            
            foreach (string s in studentQuery7)
            {
                Console.WriteLine(s);
            }
            break;
        case 9:Console.WriteLine("Những học sinh có điểm trung bình lớn hơn điểm trung bình lớp");
            foreach (var item in aboveAverageQuery)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id, item.score);
            }
        break;
    }
}
