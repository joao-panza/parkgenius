document.addEventListener('DOMContentLoaded', function() {
    carregarVeiculos(); // Chama a função ao carregar a página
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
                    <button onclick="registrarSaida('${veiculo.placa}')">Registrar Saída</button>
                </td>
            `;

                veiculosLista.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Erro ao carregar os veículos:', error);
        });



}


// Função para registrar veículo no banco de dados
function CadastrarEntrada(){
    document.getElementById('form-registro-veiculo').addEventListener('submit', async function(event) {
        event.preventDefault();
    
        const formData = new FormData(event.target);
        const veiculo = {
            placa: formData.get('placa'),
            cor: formData.get('cor'),
            marca: formData.get('marca'),
            dataEntrada: formData.get('data-entrada'),
            horaEntrada: formData.get('hora-entrada')
        };
    
        await fetch('http://localhost:5000/api/Veiculos', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(veiculo)
        });
    
        fecharModal('modal-gestao-veiculos');
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

    await fetch('http://localhost:5000/api/Funcionarios', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(funcionario)
    });    

    fecharModal('modal-cadastro-funcionario');
});
}

// Função para registrar saída do veículo
async function registrarSaida(idVeiculo) {
    await fetch(`/api/veiculos/${idVeiculo}/saida`, {
        method: 'POST'
    });
    carregarVeiculos();
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
