function atualizaElementos(campoRadio) {
  // Colocando o radio como selecionado
  campoRadio.children[0].checked = true;
  // Variaveis para os campos
  var texto = document.querySelector('#texto');
  var cifra = document.querySelector('#cifra');
  // Checando qual radio box esta selecionado
  if (document.querySelector('#rdCriptografa').checked) {
    // Radiobox do Criptografar esta selecionado
    document.querySelector('#btCodifica').textContent = 'Criptografe minha mensagem!';
    texto.classList.remove('readOnly');
    cifra.classList.add('readOnly');
    texto.removeAttribute('readonly');
    cifra.setAttribute('readonly', '');
    cifra.value = '';
    return;
  }
  // Radiobox do Descriptografar esta selecionado
  document.querySelector('#btCodifica').textContent = 'Descriptografe minha mensagem!';
  texto.classList.add('readOnly');
  cifra.classList.remove('readOnly');
  cifra.removeAttribute('readonly');
  texto.setAttribute('readonly', '');
  texto.value = '';
  return;
}

function validaChave() {
  var campoChave = document.querySelector('#campoChave');
  if (campoChave.value > 20) {
    campoChave.value = 20;
    return;
  }
  if (campoChave.value < 2) {
    campoChave.value = 2;
    return;
  }
}

function railFence() {
  if (document.querySelector('#rdCriptografa').checked) {
    // Radiobox do Criptografar esta selecionado
    var chave = document.querySelector('#campoChave').value;
    var texto = document.querySelector('#texto').value;
    var result = '';
    var i, j, iteracao1, iteracao2;
    // Primeira iteracao
    i = (chave-1)*2;
    for (j = 0; j < texto.length; j += i) {
      result += texto[j];
    }
    // Iteracoes intermediarias
    for (i = (chave-2)*2, iteracao1 = 1; i >= 2; i += -2, iteracao1 += 1) {
      for (j = iteracao1, iteracao2 = 0; j < texto.length; iteracao2 += 1) {
        result += texto[j];
        if (iteracao2 % 2 == 0) { // Iteracao par
          j += i;
        }
        else { // Iteracao impar
          j += (chave-1)*2 - i;
        }
      }
    }
    // Ultima iteracao
    i = (chave-1)*2;
    for (j = chave-1; j < texto.length; j += i) {
      result += texto[j];
    }
    document.querySelector('#cifra').value = result;
    return;
  }
  // Radiobox do Descriptografar esta selecionado
  var chave = document.querySelector('#campoChave').value;
  var cifra = document.querySelector('#cifra').value;
  var result = cifra;
  var i, j, iteracao1, iteracao2, indiceCifra = 0;
  // Primeira iteracao
  i = (chave-1)*2;
  for (j = 0; j < cifra.length; j += i) {
    result = result.replaceAt(j, cifra[indiceCifra]);
    indiceCifra += 1;
  }
  // Iteracoes intermediarias
  for (i = (chave-2)*2, iteracao1 = 1; i >= 2; i += -2, iteracao1 += 1) {
    for (j = iteracao1, iteracao2 = 0; j < cifra.length; iteracao2 += 1) {
      result = result.replaceAt(j, cifra[indiceCifra]);
      indiceCifra += 1;
      if (iteracao2 % 2 == 0) { // Iteracao par
        j += i;
      }
      else { // Iteracao impar
        j += (chave-1)*2 - i;
      }
    }
  }
  // Ultima iteracao
  i = (chave-1)*2;
  for (j = chave-1; j < cifra.length; j += i) {
    result = result.replaceAt(j, cifra[indiceCifra]);
    indiceCifra += 1;
  }
  document.querySelector('#texto').value = result;
  return;
}

String.prototype.replaceAt = function(index, character) {
    return this.substr(0, index) + character + this.substr(index + character.length);
}
