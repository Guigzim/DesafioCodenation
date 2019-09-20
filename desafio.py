import requests
import json
import hashlib
#token para conexao à API
token = '9cef946a82be7d054537cb1f466dcdaca4c6a808'
#request par receber o json
req = requests.get("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token={}".format(token))
#transformando json em um objeto
object = json.loads(req.text)

#funcao para descriptografar letra a letra
def convert(letra, casas):
    dicio = {
            'a':1,1:'a',
            'b':2,2:'b',
            'c':3,3:'c',
            'd':4,4:'d',
            'e':5,5:'e',
            'f':6,6:'f',
            'g':7,7:'g',
            'h':8,8:'h',
            'i':9,9:'i',
            'j':10,10:'j',
            'k':11,11:'k',
            'l':12,12:'l',
            'm':13,13:'m',
            'n':14,14:'n',
            'o':15,15:'o',
            'p':16,16:'p',
            'q':17,17:'q',
            'r':18,18:'r',
            's':19,19:'s',
            't':20,20:'t',
            'u':21,21:'u',
            'v':22,22:'v',
            'w':23,23:'w',
            'x':24,24:'x',
            'y':25,25:'y',
            'z':26,26:'z'
        }
    num = dicio[letra]
    num -= casas
    if num <= 0:
        num = num + 26
    return dicio[num]

decifrado = ""
#iteracao de letra em letra para descriptografar
for i in object['cifrado']:
    if i == ' ':
        decifrado +=" "
    elif i == '.':
        decifrado += "."
    else:
        decifrado += convert(i, object['numero_casas'])

#atribuindo a frase descriptografada ao objeto
object['decifrado'] = decifrado
#criando o SHA1 da frase descriptografada e atribuindo ao objeto
object['resumo_criptografico'] = hashlib.sha1(decifrado.encode('utf-8')).hexdigest()
#imprime objeto
print(object)
#cria um arquivo.json
arquivo = open('answer.json', 'w')
#escreve no arquivo o objeto convertido pra json
arquivo.write(json.dumps(object))
#fecha o arquivo
arquivo.close()
#post para upar o arquvio na API
post = requests.post('https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token={}'.format(token), files={'answer': open('answer.json', 'rb')})
#imprime o status_code da requisicao | se der 200 é pq deu boa
print(post.status_code)
print(post.text);




    

