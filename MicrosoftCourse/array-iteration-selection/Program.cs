/*
- You're developing a Student Grading application that automates the calculation of grades for each student in a class.
- Your application needs to automatically assign letter grades based on the calculated final score for each student.
- Your application needs to output/display each student’s name and formatted grade.

- Criteria for extra credit assignments:
  * Include extra credit assignment scores in the student's scores array.
  * Extra credit assignments are worth 10% of an exam score (when calculating the final numeric grade).
  * Add extra credit assignment scores to the student's total exam score before calculating the final numeric grade.
*/

int examAssignments = 5;

int[] sophiaScores = [90, 86, 87, 98, 100, 94, 90];
int[] andrewScores = [92, 89, 81, 96, 90, 89];
int[] emmaScores = [90, 85, 87, 98, 68, 89, 89, 89];
int[] loganScores = [90, 95, 87, 88, 96, 96];
int[] beckyScores = [92, 91, 90, 91, 92, 92, 92];
int[] chrisScores = [84, 86, 88, 90, 92, 94, 96, 98];
int[] ericScores = [80, 90, 100, 80, 90, 100, 80, 90];
int[] gregorScores = [91, 91, 91, 91, 91, 91, 91];

string[] studentNames = ["Sophia", "Andrew", "Emma", "Logan", "Becky", "Chris", "Eric", "Gregor"];

int[] studentScores;

string currentStudentLetterGrade;

// Report Header
Console.WriteLine("Student\t\tGrade\n");

foreach (string name in studentNames)
{
  string currentStudent = name;

  if (currentStudent == "Sophia")
    studentScores = sophiaScores;
  else if (currentStudent == "Andrew")
    studentScores = andrewScores;
  else if (currentStudent == "Emma")
    studentScores = emmaScores;
  else if (currentStudent == "Logan")
    studentScores = loganScores;
  else if (currentStudent == "Becky")
    studentScores = beckyScores;
  else if (currentStudent == "Chris")
    studentScores = chrisScores;
  else if (currentStudent == "Eric")
    studentScores = ericScores;
  else if (currentStudent == "Gregor")
    studentScores = gregorScores;
  else
    continue;

  int sumAssignmentScores = 0;

  decimal currentStudentGrade = 0;

  int gradedAssignments = 0;

  foreach (int score in studentScores)
  {
    gradedAssignments += 1;

    if (gradedAssignments <= examAssignments)
      sumAssignmentScores += score;

    else
      // add the extra credit points to the sum - bonus points equal to 10% of an exam score
      sumAssignmentScores += score / 10;
  }

  currentStudentGrade = (decimal)sumAssignmentScores / examAssignments;

  if (currentStudentGrade >= 97)
    currentStudentLetterGrade = "A+";

  else if (currentStudentGrade >= 93)
    currentStudentLetterGrade = "A";

  else if (currentStudentGrade >= 90)
    currentStudentLetterGrade = "A-";

  else if (currentStudentGrade >= 87)
    currentStudentLetterGrade = "B+";

  else if (currentStudentGrade >= 83)
    currentStudentLetterGrade = "B";

  else if (currentStudentGrade >= 80)
    currentStudentLetterGrade = "B-";

  else if (currentStudentGrade >= 77)
    currentStudentLetterGrade = "C+";

  else if (currentStudentGrade >= 73)
    currentStudentLetterGrade = "C";

  else if (currentStudentGrade >= 70)
    currentStudentLetterGrade = "C-";

  else if (currentStudentGrade >= 67)
    currentStudentLetterGrade = "D+";

  else if (currentStudentGrade >= 63)
    currentStudentLetterGrade = "D";

  else if (currentStudentGrade >= 60)
    currentStudentLetterGrade = "D-";

  else
    currentStudentLetterGrade = "F";

  Console.WriteLine($"{currentStudent}\t\t{currentStudentGrade}\t{currentStudentLetterGrade}");
}

// keeps the terminal open to view results
Console.WriteLine("\n\rPress the Enter key to continue");
Console.ReadLine();