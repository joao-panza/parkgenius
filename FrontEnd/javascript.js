document.addEventListener('DOMContentLoaded', function() {
    carregarVeiculos();
});

// Função para carregar os veículos do banco de dados
function carregarVeiculos() {
    // Faz uma requisição para a API para obter os veículos estacionados
    fetch('http://localhost:5000/api/Veiculos') // Altere para o endpoint correto
        .then(response => response.json())
        .then(data => {
            const veiculosLista = document.getElementById('veiculos-lista');
            veiculosLista.innerHTML = ''; // Limpa a tabela antes de inserir os dados

            // Itera sobre os veículos recebidos e os insere na tabela
            data.forEach(veiculo => {
                const row = document.createElement('tr');
                // Insere as colunas com os dados
                row.innerHTML = `
                <td>${veiculo.placa}</td>
                <td>${veiculo.marca}</td>    <!-- Marca primeiro -->
                <td>${veiculo.cor}</td>      <!-- Cor depois -->
                <td>${new Date(veiculo.data_entrada).toLocaleDateString()}</td>
                <td>${new Date(veiculo.data_entrada).toLocaleTimeString()}</td>
                <td>
                    <button onclick="registrarSaida('${veiculo.id_Veiculo}')">Registrar Saída</button>
                </td>
            `;

                veiculosLista.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Erro ao carregar os veículos:', error);
        });
}

function CadastrarEntrada() {
    document.getElementById('form-registro-veiculo').addEventListener('submit', async function(event) {
        event.preventDefault();

        const formData = new FormData(event.target);
        const dataEntrada = formData.get('data-entrada');
        const horaEntrada = formData.get('hora-entrada');
        
        // Combina data e hora em um único valor
        const dataHoraEntrada = new Date(`${dataEntrada}T${horaEntrada}:00`);

        const veiculo = {
            placa: formData.get('placa'),
            cor: formData.get('cor'),
            marca: formData.get('marca'),
            dataHoraEntrada: dataHoraEntrada.toISOString(),  // Enviando a data e hora de entrada
        };

        const response = await fetch('http://localhost:5000/api/Veiculos', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(veiculo)
        });

        if(response.ok)
            alert('Veiculo e entrada registrados com sucesso!');
        else
            alert('Erro ao registrar a entrada do veiculo!');

        fecharModal('modal-gestao-veiculos');
        document.getElementById('form-registro-veiculo').reset();
        carregarVeiculos();
    });
}



function CadastrarFuncionario(){
// Função para registrar funcionário no banco de dados
document.getElementById('form-cadastro-funcionario').addEventListener('submit', async function(event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const funcionario = {
        nome: formData.get('nome'),
        // matricula: formData.get('matricula'),
        telefone: formData.get('telefone'),
        email: formData.get('email'),
        login: formData.get('login'),
        senha: formData.get('senha')
    };

    const response = await fetch('http://localhost:5000/api/Funcionarios', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(funcionario)
    });    

    if(response.ok)
        alert('Funcionário Cadastrado com sucesso!');
    else
        alert('Erro ao cadastrar o funcionário');
    
    
    document.getElementById('form-cadastro-funcionario').reset();
    fecharModal('modal-cadastro-funcionario');
});
}



// Função para registrar saída do veículo (usando PUT)
async function registrarSaida(idVeiculo) {
    // Pega a hora de saída diretamente do campo de input    
    // Envia a requisição para o backend para atualizar a saída do veículo
    await fetch(`http://localhost:5000/api/Veiculos/${idVeiculo}/saida`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            registrarSaida: {
                data_saida: horaSaida  // Ajuste para enviar corretamente
            }
        })
    });

    // Atualiza a lista de veículos
    carregarVeiculos();
    
    // Fecha a modal após registrar a saída
    // fecharModal();
}


function carregarFuncionarios() {
    // Faz uma requisição para a API para obter os veículos estacionados
    fetch('http://localhost:5000/api/Funcionarios') // Altere para o endpoint correto
        .then(response => response.json())
        .then(data => {
            const funcionariosLista = document.getElementById('funcionarios-lista');
            funcionariosLista.innerHTML = ''; // Limpa a tabela antes de inserir os dados

            // Itera sobre os veículos recebidos e os insere na tabela
            data.forEach(funcionario => {
                const row = document.createElement('tr');
                // Insere as colunas com os dados
                row.innerHTML = `
                <td>${funcionario.nome}</td>        
                <td>${funcionario.telefone}</td>    
                <td>${funcionario.email}</td>      
                <td>${funcionario.login}</td>      
                <td>${funcionario.senha}</td>      
            `;

            funcionariosLista.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Erro ao carregar os funcionários:', error);
        });
}


// Função para fechar o modal
function fecharModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

// Abrir e fechar modais
const openModalButtons = document.querySelectorAll('.open-modal');
openModalButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modalId = button.getAttribute('data-modal');
        document.getElementById(modalId).style.display = 'flex';
    });
});

window.onclick = function(event) {
    const modals = document.querySelectorAll('.modal');
    modals.forEach(modal => {
        if (event.target === modal) {
            modal.style.display = 'none';
        }
    });
};

// Função de logout
function logout() {
    window.location.href = '/logout'; // Exemplo de redirecionamento para logout
}

// Carregar os veículos ao carregar a página
window.onload = carregarVeiculos;
