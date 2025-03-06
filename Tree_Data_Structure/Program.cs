//MAIN

//tworzymy nowe dzrewo i tabele z wartościami
BinaryTree drzewo1 = new BinaryTree();
int[] tabela1 = CreateVariablesTable();

//wstawiamy wartości do drzewa z tabeli
InsertIntoTree(tabela1, drzewo1);

//wyświetlanie drzewa za pomocą in-order
Console.WriteLine("Drzewo In-order:");
drzewo1.InOrder(drzewo1.Root);

Console.ReadLine();

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

//Jedna gałęź drzewa (dziecka)
public class TreeNode
{
    public int Value;
    //public int Level;
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }

    public TreeNode(int value)
    {
        this.Value = value;
        Left = null;
        Right = null;
        //Level = 1;
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
        if(node == null)
        {
            return new TreeNode(value);
        }

        if (value < node.Value) { 
            node.Left = Insert(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = Insert(node.Right, value);
        }

        ////przemieszczanie na następny poziom
        //node.Level = 1 + Math.Max(node.Left.Value, node.Right.Value);

        return node;
    }


    // Metoda do wyświetlenia drzewa  - in-order
    public void InOrder(TreeNode node)
    {
        //sprawdzenie czy drzewo nie jest puste
        if (node != null)
        {
            InOrder(node.Left); // 1) Odwiedź najpierw lewe poddrzewo
            Console.Write($"{node.Value} "); // Wydrukuj
            InOrder(node.Right); //2) Odwiedź potem prawe poddrzewo
        }
    }
}

