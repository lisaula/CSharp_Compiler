using N1.N2;

namespace Figuras{
	public abstract class Figura{
		public abstract void methodo();
		public abstract void methodo2();
		public virtual string virtualmethodo();
		private bool field;
		Figura(){

		}
	}

	enum DIASDELASEMANA{
		LUNES = 5,
		MARTES = -5,
		MIERCOLES,
		JUEVES,
		VIERNES,
		SABADO,
		DOMINGO
	}

	public interface MismoNms{
			
	}
}

public enum DIASDELASEMANA2{
		LUNES = 5,
		MARTES = -5,
		MIERCOLES,
		JUEVES = -52,
		VIERNES,
		SABADO=10,
		DOMINGO
	}

public class myClaseAloneFiguras{
	
}