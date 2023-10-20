-- criando banco de dados.

create database dbControledeConvite;

-- acessando banco de dados.

use database dbControledeConvite;

-- criando tabelas.

create table tbConvidados(
codConvidado int not null auto_increment,
nome varchar(50),
cpf char(14) not null unique,
endereco varchar(50),
bairro varchar(50),
Cep char(9),
E-mail varchar(50),
primary key(CodConvidado));

create table tbMesas(
CodMesa int not null auto_increment,
numero decimal(9,2),
primary key(codMesa),
foreign key(codConvidado) references tbConvidados(codConvidado));




