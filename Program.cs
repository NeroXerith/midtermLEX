using System;

class Program
{
    static int lastInd; 
    static void Main(string[] args)
    {
        Console.WriteLine("Choose your Sorting algorithm");
        Console.WriteLine("1. Merge Sort");
        Console.WriteLine("2. Heap Sort");

        int userInput = GetUserInput();

        switch (userInput)
        {
            case 1:
                MergeSort();
                break;
            case 2:
                HeapSort();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static int GetUserInput()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter the number of your1 choice: ");
                int input = int.Parse(Console.ReadLine());
                if (input >= 1 && input <= 2)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid integer.");
            }
        }
    }

    // Displaying array
    static void dispArr(String txt, int[] arr)
    {
        Console.Write(txt + ": ");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + ", ");
        }
        Console.WriteLine("\n");
    }


    static void MergeSort()
    {   
        Console.WriteLine("___________________");
        Console.Write("ENTER HOW MANY ELEMENTS:");
        int totalNum = Convert.ToInt32(Console.ReadLine());

        int[] numArr = new int[totalNum];

        for (int i = 0; i < numArr.Length; i++)
        {
            Console.Write($"Enter element {i + 1}: ");
            numArr[i] = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("___________________");
        Console.WriteLine("[1] ASCENDING | [2] DESCENDING");

        int orderInput = GetUserInput();

        bool ascending = orderInput == 1;

        dispArr("Initial Array", numArr);

        int[] sortedArr = arrCheck(numArr, ascending);
        dispArr("Sorted Array", sortedArr);     
                        
        Environment.Exit(0);
    }

    // Array Check
    static int[] arrCheck(int[] arr, bool ascending)
    {
        if (arr.Length > 1)
        {
            int half = arr.Length / 2;

            int[] leftSide = arrCutHalf(arr, 0, half, "Left");
            int[] rightSide = arrCutHalf(arr, half, arr.Length, "Right");

            leftSide = arrCheck(leftSide, ascending);
            rightSide = arrCheck(rightSide, ascending);

            return arrMerge(leftSide, rightSide, ascending);
        }
        else
        {
            return arr;
        }
    }

    // Array Gets Cut in Half
    static int[] arrCutHalf(int[] oldArr, int from, int to, String arrSide)
    {
        Console.WriteLine("---Cut Half---");
        int[] newArr = new int[to - from];
        Console.WriteLine("new size[" + arrSide + "]:" + newArr.Length);

        int ctr = 0;
        for (int i = from; i < to; i++)
        {
            newArr[ctr] = oldArr[i];
            ctr++;
        }
        dispArr("newArr[" + arrSide + "]", newArr);
        return newArr;
    }

    // Merge all arrays
   static int[] arrMerge(int[] leftSide, int[] rightSide, bool ascending)
{
    Console.WriteLine("\n---Merge Sort---");
    int[] arrMerge = new int[leftSide.Length + rightSide.Length];

    int indexM = 0;
    int indexL = 0;
    int indexR = 0;

    while (indexL < leftSide.Length && indexR < rightSide.Length)
    {
        int leftValue = leftSide[indexL];
        int rightValue = rightSide[indexR];

        if ((ascending && leftValue <= rightValue) ||
            (!ascending && leftValue >= rightValue))
        {
            arrMerge[indexM] = leftValue;
            Console.WriteLine($"{leftValue} > {rightValue} : {(leftValue > rightValue)}, insert {leftValue}");
            indexL++;
        }
        else
        {
            arrMerge[indexM] = rightValue;
            Console.WriteLine($"{leftValue} > {rightValue} : {(leftValue > rightValue)}, insert {rightValue}");
            indexR++;
        }

        indexM++;
    }

    while (indexL < leftSide.Length)
    {
        arrMerge[indexM] = leftSide[indexL];
        Console.WriteLine($"insert {leftSide[indexL]}");
        indexL++;
        indexM++;
    }

    while (indexR < rightSide.Length)
    {
        arrMerge[indexM] = rightSide[indexR];
        Console.WriteLine($"insert {rightSide[indexR]}");
        indexR++;
        indexM++;
    }

    dispArr("arrMerge", arrMerge);
    return arrMerge;
}

    static void HeapSort()
    {
        Console.WriteLine("___________________");
        Console.Write("ENTER HOW MANY ELEMENTS: ");
        int totalNum = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[totalNum];
        lastInd = arr.Length;

        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write($"Enter element {i + 1}: ");
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        dispArr("Original: ", arr);

        int choiceNum;

        while (true)
        {
            Console.WriteLine("\n___________________");
            Console.WriteLine("[1] ASCENDING | [2] DESCENDING");
            Console.Write("Choice: ");

            if (int.TryParse(Console.ReadLine(), out choiceNum))
            {
                switch (choiceNum)
                {
                    case 1:
                        Console.WriteLine("You chose ASCENDING");
                        for (int i = 0; i < arr.Length; i++)
                        {
                            Console.WriteLine("---");

                            arr = maxHeap(arr);
                            dispArr("Max Heap[" + (i + 1) + "]", arr);

                            arr = heapify(arr);
                            dispArr("Heapify[" + (i + 1) + "]", arr);
                        }
                        break;
                    case 2:
                        Console.WriteLine("You chose DESCENDING");
                        for (int i = 0; i < arr.Length; i++)
                        {
                            Console.WriteLine("---");

                            arr = minHeap(arr);
                            dispArr("Min Heap[" + (i + 1) + "]", arr);

                            arr = heapify(arr);
                            dispArr("Heapify[" + (i + 1) + "]", arr);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. 1 or 2 only.");
                        break;
                }

                if (choiceNum == 1 || choiceNum == 2)
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. 1 or 2 only.");
            }
        }
    }

    // -- [1] Heap Sort --

    // Min Heap
    static int[] minHeap(int[] arr)
    {
        for (int i = lastInd; i > 1; i--)
        {
            int parent = i / 2;
            int LChild = 2 * parent;
            int RChild = 2 * parent + 1;
            parent--;
            LChild--;
            RChild--;

     
            bool hasRChild = false;
            if (parent < lastInd)
            {
                // Console.WriteLine("P:" + arr[parent]);
            }
            if (LChild < lastInd)
            {
                // Console.WriteLine("LC:" + arr[LChild]);
            }
            if (RChild < lastInd)
            {
                // Console.WriteLine("RC:" + arr[RChild]);
                hasRChild = true;
            }
            if (hasRChild)
            {
                if (arr[parent] > arr[RChild] && arr[RChild] < arr[LChild])
                {
                    int temp = arr[parent];
                    arr[parent] = arr[RChild];
                    arr[RChild] = temp;
                }
            }
            if (arr[parent] > arr[LChild])
            {
                int temp = arr[parent];
                arr[parent] = arr[LChild];
                arr[LChild] = temp;
            }
        }

        return arr;
    }

    // Max Heap
    static int[] maxHeap(int[] arr)
    {
        for (int i = lastInd; i > 1; i--)
        {
            int parent = (i / 2);
            int LChild = (2 * parent);
            int RChild = (2 * parent + 1);
            parent--;
            LChild--;
            RChild--;

            bool hasRChild = false;
            if (parent < lastInd)
            {
                // Console.WriteLine("P:" + arr[parent]);
            }
            if (LChild < lastInd)
            {
                // Console.WriteLine("LC:" + arr[LChild]);
                // hasLChild = true;
            }
            if (RChild < lastInd)
            {
                // Console.WriteLine("RC:" + arr[RChild]);
                hasRChild = true;
            }
            if (hasRChild)
            {
                if (arr[parent] < arr[RChild] && arr[RChild] > arr[LChild])
                {
                    int temp = arr[parent];
                    arr[parent] = arr[RChild];
                    arr[RChild] = temp;
                }
            }
            if (arr[parent] < arr[LChild])
            {
                int temp = arr[parent];
                arr[parent] = arr[LChild];
                arr[LChild] = temp;
            }
        }

        return arr;
    }

    // Heapify
    static int[] heapify(int[] arr)
    {
        int temp = arr[0];
        arr[0] = arr[lastInd - 1];
        arr[lastInd - 1] = temp;

        lastInd--;

        return arr;
    }
}
