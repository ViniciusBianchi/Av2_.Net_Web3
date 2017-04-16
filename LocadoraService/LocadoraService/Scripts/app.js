var ViewModel = function () {
    var self = this;
    self.filmes = ko.observableArray();
    self.locadoras = ko.observableArray();
    self.clientes = ko.observableArray();
    self.error = ko.observable();

    self.newFilme = {
        Id: ko.observable(),
        Nome: ko.observable(),
        Categoria: ko.observable(),
        FaixaEtaria: ko.observable(),
        Preco: ko.observable()
    }

    self.newLocadora = {
        Id: ko.observable(),
        Nome: ko.observable(),
        Endereco: ko.observable()
    }

    self.newCliente = {
        Id: ko.observable(),
        Nome: ko.observable(),
        CPF: ko.observable(),
        FilmeId: ko.observable(),
        NomeDoFilme: ko.observable()
    }

    var filmesUri = '/api/filmes/';
    var locadorasUri = '/api/locadoras/';
    var clientesUri = '/api/clientes/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllFilmes() {
        ajaxHelper(filmesUri, 'GET').done(function (data) {
            self.filmes(data);
        });
    }

    function getAllLocadoras() {
        ajaxHelper(locadorasUri, 'GET').done(function (data) {
            self.locadoras(data);
        });
    }

    function getAllClientes() {
        ajaxHelper(clientesUri, 'GET').done(function (data) {
            self.clientes(data);
        });
    }

    self.addFilme = function (formElement) {
        var filme = {
            Id: self.newFilme.Id,
            Nome: self.newFilme.Nome,
            Categoria: self.newFilme.Categoria,
            FaixaEtaria: self.newFilme.FaixaEtaria,
            Preco: self.newFilme.Preco
        };

        ajaxHelper(filmesUri, 'POST', filme).done(function (item) {
            self.filmes.push(item);
        });
    }

    self.addLocadora = function (formElement) {
        var locadora = {
            Id: self.newLocadora.Id,
            Nome: self.newLocadora.Nome,
            Endereco: self.newLocadora.Endereco,
        };

        ajaxHelper(locadorasUri, 'POST', locadora).done(function (item) {
            self.locadoras.push(item);
        });
    }

    self.addCliente = function (formElement) {
        var cliente = {
            Id: self.newCliente.Id,
            Nome: self.newCliente.Nome,
            CPF: self.newCliente.CPF,
            FilmeId: self.newCliente.FilmeId,
            NomeDoFilme: self.newCliente.NomeDoFilme
        };

        ajaxHelper(clientesUri, 'POST', cliente).done(function (item) {
            self.clientes.push(item);
        });
    }

    self.detail = ko.observable();
    self.detailFilmes = ko.observable();
    self.detailLocadoras = ko.observable();
    self.detailClientes = ko.observable();

    self.getFilmeDetail = function (item) {
        ajaxHelper(filmesUri + item.Id, 'GET').done(function (data) {
            self.detailFilmes(data);
        });
    }

    self.getLocadoraDetail = function (item) {
        ajaxHelper(locadorasUri + item.Id, 'GET').done(function (data) {
            self.detailLocadoras(data);
        });
    }

    self.getClienteDetail = function (item) {
        ajaxHelper(clientesUri + item.Id, 'GET').done(function (data) {
            self.detailClientes(data);
        });
    }
    // Fetch the initial data.
    getAllFilmes();
    getAllLocadoras();
    getAllClientes();
};

ko.applyBindings(new ViewModel());