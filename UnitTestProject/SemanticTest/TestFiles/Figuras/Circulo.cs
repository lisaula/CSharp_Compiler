using N1;

namespace Figuras{
	public class Circulo : Figura{
		public int field1;
		public string field2;
		public static float field3;
		public bool field4;
		private Types t;
		myClase[,,][][] field5 = new myClase[3,2][][]{{5,2}, {2,3}, {4,5}};

	}
	namespace insideFiguras{
		public interface Types: myInterface{
			
		}
	}
	public interface MismoTypes: myInterface,MismoNms, outsideInterface{
			
	}
}
public interface myInterface : Types{
	void getNombre(int index);
	void getNompre(string name);
	void getNombre(int[] index);
	void getNombre(int[,,][] index);
	int setIndex(int index);
}

public interface Types{
			
}