describe("A suite", function () {
    it("contains spec with an expectation", function () {
        expect(true).toBe(true);
    });

});

describe("Another suite", function () {
    var resultado;
    it("prueba de peticion REST", function (done) {        
        $.get("api/values/3", function (data) {
            resultado = data;
            done();//Hasta que no termina la funcion  nada
        })        
    });

    afterEach(function (done) {
        expect(resultado).toBe("value");
        done();
    }, 1000);
});

describe("GET todos", function () {
    var resultado;
    it("GET Todos", function (done) {
        $.get("api/values", function (data) {
            resultado = data;
            done();//Hasta que no termina la funcion  nada
        })
    });

    afterEach(function (done) {
        expect(resultado[0]).toBe("valueMio");
        expect(resultado[1]).toBe("valueOtro");
        done();
    }, 1000);
});

describe("POST suite", function () {
    var resultado;
    it("prueba con POST", function (done) {
        $.post("api/values", function (data) {
            resultado = data;
            done();//Hasta que no termina la funcion  nada
        })
    });

    afterEach(function (done) {
        expect(resultado).toBe(resultado);
        done();
    }, 1000);
});

//describe("PUT suite", function () {
//    var resultado;
//    it("prueba con PUT", function (done) {
//        $.ajax({
//            url: 'api/values/3',
//            type: 'PUT',
//            success: function (data) {
//                resultado = data;
//                done()
//            }
//        });
//    });

//    afterEach(function (done) {
//        expect(resultado).toBe(resultado);
//        done();
//    }, 1000);
//});

describe("Delete suite", function () {
    var resultado;
    it("prueba con Delete", function () {
        $.ajax({
            url: 'api/values/3',
            type: 'DELETE',
            success: function (data) {
                resultado = data;
                done()
            }
        });
    });

    afterEach(function (done) {
        expect(resultado).toBe(resultado);
        done();
    }, 1000);
});

describe("Get entradas", function () {
    var resultado;
    it("Get entrada Unica", function (done) {
        var params = {
            Precio: 3.4
        };
        $.post("api/Entradas", params, function (data) {
            resultado = data;
            done();//Hasta que no termina la funcion  nada
        })
    });

    afterEach(function (done) {
        expect(resultado.Id != undefined).toBe(true);
        done();
    }, 1000);
});