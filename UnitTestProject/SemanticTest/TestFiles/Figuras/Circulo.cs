using N1;

namespace Figuras{
	public class Circulo : Figura{
		public int field1;
		public string field2;
		public static float field3;
		public bool field4;
		private Types t;
		myClase[,,] field5 = new myClase[5,10][][];

	}
	namespace insideFiguras{
		public interface Types{
			
		}
	}
}
public interface myInterface : Types, outsideInterface{
	void getNombre(int index);
	void getNompre(string name);
	int setIndex(int index);
}

public interface Types{
			
}