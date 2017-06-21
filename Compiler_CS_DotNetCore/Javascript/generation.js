let {
    itoa,
    atoi,
    toInt
} = require("./Utils.js");
let GlobalNamespace = {};
GlobalNamespace.System = {};
GlobalNamespace.System.IO = {};
GlobalNamespace.Object = class {
    Object() {}
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.IntType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GlobalNamespace.CharType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return s;
    }
}
GlobalNamespace.FloatType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GlobalNamespace.StringType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.BoolType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.System.Console = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
    }
    static ReadLine() {}
}
GlobalNamespace.System.IO.TextWriter = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
    }
}
GlobalNamespace.System.Console.Out = new GlobalNamespace.System.IO.TextWriter();
GlobalNamespace.System.IO.TextReader = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static ReadLine() {}
}
GlobalNamespace.sort = class extends GlobalNamespace.Object {
    sort() {}
    constructor() {
        super()
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static MainStringTypeArray1D(args) {
        let c7 = toInt(atoi('a') + (1));
        let c = toInt((20) + (30));
        let c1 = toInt(((toInt(((toInt((toInt((20) + (30))) - (toInt((10) * (50)))))) / (5)))) * (10));
        let c2 = toInt((1) << (0x1));
        let c3 = toInt((c2) ^ (c1));
        let c4 = toInt((1) & (c));
        let c5 = toInt((100) % (7));
        let c6 = toInt((c4) | (c1));
        let c8 = toInt(atoi('b') * atoi('r'));
        GlobalNamespace.System.Console.WriteLineStringType((("Suma: ") + itoa(c)));
        GlobalNamespace.System.Console.WriteLineStringType((("sumatoria complicada: ") + itoa(c1)));
        GlobalNamespace.System.Console.WriteLineStringType((("left shifting: ") + itoa(c2)));
        GlobalNamespace.System.Console.WriteLineStringType((("XOR operator: ") + itoa(c3)));
        GlobalNamespace.System.Console.WriteLineStringType((("AND operator: ") + itoa(c4)));
        GlobalNamespace.System.Console.WriteLineStringType((("MOD operator: ") + itoa(c5)));
        GlobalNamespace.System.Console.WriteLineStringType((("OR operator: ") + itoa(c6)));
        GlobalNamespace.System.Console.WriteLineStringType((("Suma char and int: ") + itoa(c7)));
        GlobalNamespace.System.Console.WriteLineStringType((("Mult chars: ") + itoa(c8)));
        let s = (("a") + itoa(1));
        let s2 = (("a") + itoa(1.5));
        GlobalNamespace.System.Console.WriteLineStringType((("Suma string-float ") + (s2)));
        s2 += " fin";
        GlobalNamespace.System.Console.WriteLineStringType((("Suma string-int ") + (s)));
        GlobalNamespace.System.Console.WriteLineStringType((("Suma assign ") + (s2)));
        GlobalNamespace.System.Console.WriteLineStringType("Using selectionsort ");
        let array = [7, 50, 20, 40, 90, 6, 4];
        let size = 7;
        GlobalNamespace.sort.IntArraySelectionSortIntTypeArray1DIntType(array, size);
        for (let i = 0;
            ((i) < (size)); i++) {
            GlobalNamespace.System.Console.WriteLineStringType((("") + itoa(array[toInt(i)])));
        };
        GlobalNamespace.System.Console.WriteLineStringType("Using quicksort ");
        let array2 = [7, 35, 22, 45, 92, 11, 4];
        let size2 = 7;
        GlobalNamespace.sort.IntArrayQuickSortIntTypeArray1DIntType(array2, size2);
        for (let i = 0;
            ((i) < (size2)); i++) {
            GlobalNamespace.System.Console.WriteLineStringType((("") + itoa(array2[toInt(i)])));
        };
    }
    static IntArrayQuickSortIntTypeArray1DIntTypeIntType(data, l, r) {
        let i = null
        let j = null;
        let x = null;
        i = l;
        j = r;
        x = data[toInt(toInt(((toInt((l) + (r)))) / (2)))];
        while (true) {
            while (((data[toInt(i)]) < (x))) {
                i++
            };
            while (((x) < (data[toInt(j)]))) {
                j--
            };
            if (((i) <= (j))) {
                GlobalNamespace.sort.exchangeIntTypeArray1DIntTypeIntType(data, i, j);
                i++;
                j--;
            };
            if (((i) > (j))) {
                break
            };
        };
        if (((l) < (j))) {
            GlobalNamespace.sort.IntArrayQuickSortIntTypeArray1DIntTypeIntType(data, l, j)
        };
        if (((i) < (r))) {
            GlobalNamespace.sort.IntArrayQuickSortIntTypeArray1DIntTypeIntType(data, i, r)
        };
    }
    static IntArrayQuickSortIntTypeArray1DIntType(data, size) {
        GlobalNamespace.sort.IntArrayQuickSortIntTypeArray1DIntTypeIntType(data, 0, toInt((size) - (1)));
    }
    static IntArrayMinIntTypeArray1DIntTypeIntType(data, start, size) {
        let minPos = start;
        for (let pos = toInt((start) + (1));
            ((pos) < (size)); pos++) {
            if (((data[toInt(pos)]) < (data[toInt(minPos)]))) {
                minPos = pos
            }
        };
        return minPos;
    }
    static IntArraySelectionSortIntTypeArray1DIntType(data, size) {
        let i = null;
        let N = size;
        GlobalNamespace.System.Console.WriteLineStringType("Hola");
        let n = GlobalNamespace.IntType.ParseStringType("5");
        for (i = 0;
            ((i) < (toInt((N) - (1)))); i++) {
            let k = this.IntArrayMinIntTypeArray1DIntTypeIntType(data, i, size);
            if (((i) != (k))) {
                GlobalNamespace.sort.exchangeIntTypeArray1DIntTypeIntType(data, i, k)
            };
        };
    }
    static exchangeIntTypeArray1DIntTypeIntType(data, m, n) {
        let temporary = null;
        temporary = data[toInt(m)];
        data[toInt(m)] = data[toInt(n)];
        data[toInt(n)] = temporary;
    }
}
module.exports = GlobalNamespace;