using N1;
using N1.N2;
using System;
namespace Figuras{
	public class Circulo : Figura, MismoTypes{
		int[] n1 = new int[4] {2, 4, 6, 8};
		int[] n2 = new int[] {2, 4, 6, 8};
		int[] n3 = {2, 4, 6, 8};
		// Single-dimensional array (strings).
		string[] s1 = new string[3] {"John", "Paul", "Mary"};
		string[] s2 = new string[] {"John", "Paul", "Mary"};
		string[] s3 = {"John", "Paul", "Mary"};

		// Multidimensional array.
		int[,] n4 = new int[3, 2] { {1, 2}, {3, 4}, {5, 6} };
		public int[,] n5 = new int[,] { {1, 2}, {3, 4}, {5, 6} };
		int[,] n6 = { {1, 2}, {3, 4}, {5, 6} };

		// Jagged array.
		int[][] n7 = new int[2][] { new int[] {2,4,6}, new int[] {1,3,5,7,9} };
		int[][] n8 = new int[][] { new int[] {2,4,6}, new int[] {1,3,5,7,9} };
		int[][] n9 = { new int[] {2,4,6}, new int[] {1,3,5,7,9} };

		public int field1;
		public string field2;
		public static float field3;
		public int field4 = myClase.ca;
		
		myClase nueva = new myClase();
		myClase[] field5 = new myClase[3];
		int[,] x = { {1,2,3} , {4,5,6} };
		int[][] x2 = new int[2][]{new int[2], new int[2]};
		
		public override void methodo(){}
		public override void methodo2(){}
		public override string virtualmethodo(){}

		public virtual void MismoMethodo(){}
		public void MismoMethodo2(){}
		public Circulo(Figura f, int n, string name) : base(){
		}

		Circulo(myClase2 d){
			int[] jaja = n7[x[0,0]];
			int jaja1 = n6[0,n5[0,0]];
			Console.WriteLine(""+5);
		}

		void meth(){}

	}
	namespace insideFiguras{
		public interface Types: myInterface{
			
		}
	}
	public interface MismoTypes: myInterface,MismoNms, outsideInterface{
			void MismoMethodo();
			void MismoMethodo2();
	}
}
public interface child{}

public interface myInterface : Types{
	void getNombre(int index);
	/*void getNompre(string name);
	void getNombre(int[] index);
	void getNombre(int[,,][] index);
	int setIndex(int index);*/
}

public interface Types: child{

}
public abstract class nn{
	protected abstract void methodo();
}
public abstract class asbtracta : nn, myInterface{
	protected override void methodo(){

	}
	void getNombre(int index){

	}
}