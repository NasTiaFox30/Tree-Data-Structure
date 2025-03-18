using System.Xml.Linq;
string scissors = "8<-------------------------------------------------------------------------";

//MAIN

//tworzymy nowe dzrewo i tabele z wartościami
BinaryTree drzewo1 = new BinaryTree();
Console.WriteLine(scissors);

int[] tabela1 = CreateVariablesTable();
int[] tabela2 = [45, 27, 67, 36, 56, 15, 75, 31, 53, 39, 64]; //zabalsowany
int[] tabela3 = [5, 12, 3, 8, 20, 1, 15, 7, 10, 98, 99]; //nie zbalansowany

//wstawiamy wartości do drzewa z tabeli
InsertIntoTree(tabela1, drzewo1);
//InsertIntoTree(tabela2, drzewo1);
//InsertIntoTree(tabela3, drzewo1);

//wyświetlanie drzewa za pomocą in-order
Console.WriteLine("\nDrzewo In-order:");
drzewo1.InOrder(drzewo1.Root);

Console.WriteLine("\nDrzewo Post-order:");
drzewo1.PostOrder(drzewo1.Root);

//sprawdzenie stanu drzewa:
Console.WriteLine();
IsBalanced(drzewo1);

Console.WriteLine();
Console.WriteLine(scissors);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("CopyRights Anastasiia Bzova 66617");
Console.ReadLine();
Console.ResetColor();




//

//Statyczne metody
static int[] CreateVariablesTable()
{
    int[] newtable;
    Console.Write("Podaj długość ciągu - ");
    int length_of_tree = Convert.ToInt32(Console.ReadLine());
    newtable = new int[length_of_tree];

    Console.WriteLine("Podaj liczby: ");
    //pomieszczanie liczb do tablicy
    for (int i = 0; i < length_of_tree; i++)
    {
        newtable[i] = Convert.ToInt32(Console.ReadLine());
    }
    return newtable;
}

static void InsertIntoTree(int[] table, BinaryTree tree)
{
    //pomieszczanie wartości do drzewa
    foreach (int wartosc in table)
    {
        tree.Insert(wartosc);
    }
}

static void IsBalanced(BinaryTree tree)
{
    Console.WriteLine("\nStan drzewa:");
    int stan = tree.BalanceFactor(tree.Root);

    if (stan == 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("~ Zbalansowane! ~");
        Console.ResetColor();
        InsertElem(tree);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("~ Nie zbalasowane! ~");
        Console.ResetColor();
        Console.WriteLine();

        //zbalansowanie:
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Zbalansowanie:");
        Console.ResetColor();
        tree.BalanceTree();

        Console.WriteLine("\nDrzewo In-order (zbalansowane):");
        tree.InOrder(tree.Root);

        Console.WriteLine("\nDrzewo Post-order (zbalansowane):");
        tree.PostOrder(tree.Root);
    }
}

static void InsertElem(BinaryTree tree)
{
    Console.WriteLine("Dodaj elemnet dla zdezbalansowania:");
    tree.Insert(Convert.ToInt32(Console.ReadLine()));
    IsBalanced(tree);   //sparawdzenie zbalansowania drzewa 
}


//Jedna gałęź drzewa (dziecka)
public class TreeNode
{
    public int Value;
    //public int Level;
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }

    public int Height { get; set; }
    public TreeNode(int value)
    {
        this.Value = value;
        Left = null;
        Right = null;
        Height = 1;     //ustawiamy wysokość na 1
    }

}

//Binarne drzewo
public class BinaryTree
{
    public TreeNode Root { get; set; }

    //wstawianie wartości w ROOT
    public void Insert(int value)
    {
        Root = Insert(Root, value);
    }
    //wstawianie wartości w dziecko - NODE (lewy lub prawy)
    public TreeNode Insert(TreeNode node, int value)
    {
        if (node == null)
        {
            return new TreeNode(value);     //umieszczamy wartość w pustą komurkę
        }

        if (value < node.Value)
        {
            node.Left = Insert(node.Left, value);       //umieść w lewą stronę
        }
        else if (value > node.Value)
        {
            node.Right = Insert(node.Right, value);     //umieść w prawą stronę
        }

        ////przemieszczanie na następny poziom
        int leftHeight = node.Left != null ? node.Left.Height : 0;
        int rightHeight = node.Right != null ? node.Right.Height : 0;
        int maxHeight = Math.Max(leftHeight, rightHeight); //zwraca maksymalną wysokość pomiędzy dwoma dziećmi
        node.Height = 1 + maxHeight; //umieszcza dziecko na następny poziom

        return node;

    }


    // Metoda do wyświetlenia drzewa  - in-order
    public void InOrder(TreeNode node)
    {
        //sprawdzenie czy nie jest puste
        if (node != null)
        {
            InOrder(node.Left); //lewe poddrzewo
            Console.Write($"{node.Value} "); //Wydrukuj
            InOrder(node.Right); //prawe poddrzewo
        }
    }
    public void PostOrder(TreeNode node)
    {
        //sprawdzenie czy nie jest puste
        if (node != null)
        {
            PostOrder(node.Left); //lewe poddrzewo
            PostOrder(node.Right); //prawe poddrzewo
            Console.Write($"{node.Value} "); //Wydrukuj
        }
    }

    public int BalanceFactor(TreeNode node)
    {
        if (node == null)
        {
            return 0;
        }
        int leftH = node.Left != null ? node.Left.Height : 0;
        int rightH = node.Right != null ? node.Right.Height : 0;

        int balancefactor = leftH - rightH;      //zwraca balance factor

        return balancefactor;

    }


    //Right rotation
    public TreeNode RightRotate(TreeNode y)
    {
        TreeNode temp;
        TreeNode x = y.Left;
        if (x != null)
        {
            temp = x.Right;
            x.Right = y;
            y.Left = temp;
        }

        int leftY = y.Left != null ? y.Left.Height : 0;
        int rightY = y.Right != null ? y.Right.Height : 0;
        int leftX = x.Left != null ? x.Left.Height : 0;
        int rightX = x.Right != null ? x.Right.Height : 0;


        y.Height = Math.Max(leftY, rightY) + 1;
        x.Height = Math.Max(leftX, rightX) + 1;

        return x;
    }

    //Left rotation
    private TreeNode LeftRotate(TreeNode x)
    {
        TreeNode temp;
        TreeNode y = x.Right;
        if (y != null)
        {
            temp = y.Left;
            y.Left = x;
            x.Right = temp;
        }

        int leftY = y.Left != null ? y.Left.Height : 0;
        int rightY = y.Right != null ? y.Right.Height : 0;
        int leftX = x.Left != null ? x.Left.Height : 0;
        int rightX = x.Right != null ? x.Right.Height : 0;

        y.Height = Math.Max(leftY, rightY) + 1;
        x.Height = Math.Max(leftX, rightX) + 1;

        return y;
    }

    //Metoda do zbalansowania drzewa
    public void BalanceTree()
    {
        Root = BalanceTree(Root);
    }
    public TreeNode BalanceTree(TreeNode node)
    {

        // Balance the left and right subtrees

        if (node == null)
        {
            return null;
        }

        node.Left = BalanceTree(node.Left);
        node.Right = BalanceTree(node.Right);


        // Get the balance factor
        int balance = BalanceFactor(node);

        // Left Left
        if (balance > 1 && BalanceFactor(node.Left) > 0)
        {
            return RightRotate(node);
        }

        // Right Right 
        if (balance < -1 && BalanceFactor(node.Right) < 0)
        {
            return LeftRotate(node);
        }

        // Left Right
        if (balance > 1 && BalanceFactor(node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // Right Left 
        if (balance < -1 && BalanceFactor(node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }
}

