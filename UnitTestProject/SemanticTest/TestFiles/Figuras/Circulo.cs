using N1;
using N1.N2;
namespace Figuras{
	public class Circulo : Figura, MismoTypes{
		public int field1;
		public string field2;
		public static float field3;
		public bool field4;
		private Types t;
		myClase[,,][][] field5 = new myClase[3,2][][]{{5,2}, {2,3}, {4,5}};
		
		public override void methodo(){}
		public override void methodo2(){}
		public override string virtualmethodo();

		public virtual void MismoMethodo(){}
		public void MismoMethodo2(){}
		public Circulo(Figura f, int n, string name) : base(){

		}

		public Circulo(myClase2 d){

		}

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
	void getNompre(string name);
	void getNombre(int[] index);
	void getNombre(int[,,][] index);
	int setIndex(int index);
}

public interface Types: child{
			
}